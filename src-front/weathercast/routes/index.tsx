import { config } from '../config.ts';

type WeatherForecast = {
  date: string;
  temperatureC: number;
  town : string;
  postalCode : string
  summary: string | null;
};

const API_URL = config.apiulr;

export default async function Home() {
  let data: WeatherForecast[] = [];
  let errorMessage = "";

  try {
    console.log(API_URL);
    const res = await fetch(API_URL);

    if (!res.ok) {
      errorMessage = `API indisponible (${res.status} ${res.statusText})`;
      console.log(errorMessage);
    } else {
      data = (await res.json()) as WeatherForecast[];
    }
  } catch (e) {
    console.log(e)
    errorMessage = "Impossible de joindre l’API.";
  }

  return (
    <main style={{ padding: "2rem", fontFamily: "system-ui" }}>
      <h1>Weather Forecast</h1>

      {errorMessage && (
        <p style={{ color: "crimson", marginBottom: "1rem" }}>
          {errorMessage}
        </p>
      )}

      {data.length === 0 ? (
        <p>Aucune donnée disponible pour le moment.</p>
      ) : (
        <table border={1} cellPadding={10} style={{ borderCollapse: "collapse" }}>
          <thead>
            <tr>
              <th>Date</th>
              <th>Température C</th>
              <th>Ville</th>
              <th>Code Postal</th>
              <th>Résumé</th>
            </tr>
          </thead>
          <tbody>
            {data.map((item) => (
              <tr key={item.date}>
                <td>{new Date(item.date).toLocaleDateString("fr-FR")}</td>
                <td>{item.temperatureC}</td>
                <td>{item.town}</td>  
                <td>{item.postalCode}</td>
                <td>{item.summary ?? "-"}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </main>
  );
}
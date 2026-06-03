import { config } from '../config.ts';

type WeatherForecast = {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string | null;
};

const API_URL = config.apiulr;

export default async function Home() {
  let data: WeatherForecast[] = [];
  let errorMessage = "";

  try {
    const res = await fetch(API_URL);

    if (!res.ok) {
      errorMessage = `API indisponible (${res.status} ${res.statusText})`;
    } else {
      data = (await res.json()) as WeatherForecast[];
    }
  } catch {
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
              <th>Température F</th>
              <th>Résumé</th>
            </tr>
          </thead>
          <tbody>
            {data.map((item) => (
              <tr key={item.date}>
                <td>{new Date(item.date).toLocaleDateString("fr-FR")}</td>
                <td>{item.temperatureC}</td>
                <td>{item.temperatureF}</td>
                <td>{item.summary ?? "-"}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </main>
  );
}
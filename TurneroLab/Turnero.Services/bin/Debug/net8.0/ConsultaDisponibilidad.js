import { useState } from "react";
import { useEquipos } from "../context/EquiposContext";

// Simulación de reservas (esto se reemplazará por datos del backend)
const reservasSimuladas = [
  // { maquina: "CNC Router STM", fecha: "24/06/2025" },
  // { maquina: "CNC Router STM", fecha: "26/06/2025" },
  // { maquina: "Impresora 3D", fecha: "25/06/2025" },
];

// Utilidad para obtener las fechas de la semana actual (lunes a domingo)
function getWeekDates() {
  const hoy = new Date();
  const diaSemana = hoy.getDay(); // 0=Domingo, 1=Lunes...
  const lunes = new Date(hoy);
  lunes.setDate(hoy.getDate() - ((diaSemana + 6) % 7));
  return Array.from({ length: 7 }, (_, i) => {
    const d = new Date(lunes);
    d.setDate(lunes.getDate() + i);
    return d;
  });
}

// Función para formatear la fecha a DD/MM/YYYY
function formatearFecha(fecha) {
  const dia = fecha.getDate().toString().padStart(2, '0');
  const mes = (fecha.getMonth() + 1).toString().padStart(2, '0');
  const anio = fecha.getFullYear();
  return `${dia}/${mes}/${anio}`;
}

export default function DisponibilidadSemana() {
  const { equipos } = useEquipos();
  const [maquina, setMaquina] = useState(equipos[0]?.nombre || "");
  const semana = getWeekDates();

  if (!equipos.length) {
    return (
      <div className="container mt-5">
        <h4>Disponibilidad semanal de equipos</h4>
        <div className="alert alert-warning">No hay equipos registrados.</div>
      </div>
    );
  }

  return (
    <div className="container mt-5">
      <h4 className="mb-4 text-center">Disponibilidad semanal de equipos</h4>
      <div className="mb-4 d-flex flex-column flex-md-row align-items-center gap-3">
        <label className="form-label mb-0" htmlFor="maquinaSelect">
          Selecciona una máquina:
        </label>
        <select
          id="maquinaSelect"
          className="form-select"
          style={{ maxWidth: 300 }}
          value={maquina}
          onChange={e => setMaquina(e.target.value)}
        >
          {equipos.map(eq => (
            <option key={eq.nombre} value={eq.nombre}>{eq.nombre}</option>
          ))}
        </select>
      </div>
      <div className="table-responsive">
        <table className="table table-bordered text-center align-middle">
          <thead className="table-light">
            <tr>
              {["Lunes","Martes","Miércoles","Jueves","Viernes","Sábado","Domingo"].map(dia => (
                <th key={dia}>{dia}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            <tr>
              {semana.map(fecha => {
                const fechaFormateada = formatearFecha(fecha);
                const reservada = reservasSimuladas.some(
                  r => r.maquina === maquina && r.fecha === fechaFormateada
                );
                return (
                  <td
                    key={fechaFormateada}
                    style={{
                      background: reservada ? "#f87171" : "#bbf7d0",
                      minWidth: 120,
                      fontWeight: "bold"
                    }}
                  >
                    {reservada ? (
                      <>
                        <span style={{ color: "#b91c1c" }}>Reservado</span>
                        <br />
                        <small>{fechaFormateada}</small>
                      </>
                    ) : (
                      <>
                        <span style={{ color: "#166534" }}>Disponible</span>
                        <br />
                        <small>{fechaFormateada}</small>
                      </>
                    )}
                  </td>
                );
              })}
            </tr>
          </tbody>
        </table>
      </div>
      <div className="text-muted text-center mt-3">
        <small>
          Cuando se conecte el backend, aquí se mostrará la disponibilidad real de cada equipo.
        </small>
      </div>
    </div>
  );
}
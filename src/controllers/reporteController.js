const Reserva = require('../models/reserva');
const Equipo = require('../models/equipo');
const Usuario = require('../models/usuario');
const Evento = require('../models/evento');

// Ejemplo: Reporte de reservas por usuario
exports.reporteReservasPorUsuario = async (req, res) => {
  const reservas = await Reserva.findAll({
    include: [{ model: Usuario }, { model: Equipo }]
  });
  res.json(reservas);
};

// Ejemplo: Reporte de uso de equipos
exports.reporteUsoEquipos = async (req, res) => {
  const reservas = await Reserva.findAll({
    include: [{ model: Equipo }]
  });
  res.json(reservas);
};

// Ejemplo: Reporte de eventos en un periodo
exports.reporteEventosPorFecha = async (req, res) => {
  const { fechaInicio, fechaFin } = req.query;
  const eventos = await Evento.findAll({
    where: {
      fecha: {
        [require('sequelize').Op.between]: [fechaInicio, fechaFin]
      }
    }
  });
  res.json(eventos);
};
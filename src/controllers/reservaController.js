const Reserva = require('../models/reserva');

exports.getReservas = async (req, res) => {
  const reservas = await Reserva.findAll();
  res.json(reservas);
};

exports.createReserva = async (req, res) => {
  const reserva = await Reserva.create(req.body);
  res.status(201).json(reserva);
};

exports.updateReserva = async (req, res) => {
  const { id } = req.params;
  await Reserva.update(req.body, { where: { id } });
  res.json({ mensaje: 'Reserva actualizada' });
};

exports.deleteReserva = async (req, res) => {
  const { id } = req.params;
  await Reserva.destroy({ where: { id } });
  res.json({ mensaje: 'Reserva eliminada' });
};
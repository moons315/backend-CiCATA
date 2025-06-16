const Evento = require('../models/evento');

exports.getEventos = async (req, res) => {
  const eventos = await Evento.findAll();
  res.json(eventos);
};

exports.createEvento = async (req, res) => {
  const evento = await Evento.create(req.body);
  res.status(201).json(evento);
};

exports.updateEvento = async (req, res) => {
  const { id } = req.params;
  await Evento.update(req.body, { where: { id } });
  res.json({ mensaje: 'Evento actualizado' });
};

exports.deleteEvento = async (req, res) => {
  const { id } = req.params;
  await Evento.destroy({ where: { id } });
  res.json({ mensaje: 'Evento eliminado' });
};
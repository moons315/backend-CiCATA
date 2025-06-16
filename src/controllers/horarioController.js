const Horario = require('../models/horario');

exports.getHorarios = async (req, res) => {
  const horarios = await Horario.findAll();
  res.json(horarios);
};

exports.createHorario = async (req, res) => {
  const horario = await Horario.create(req.body);
  res.status(201).json(horario);
};

exports.updateHorario = async (req, res) => {
  const { id } = req.params;
  await Horario.update(req.body, { where: { id } });
  res.json({ mensaje: 'Horario actualizado' });
};

exports.deleteHorario = async (req, res) => {
  const { id } = req.params;
  await Horario.destroy({ where: { id } });
  res.json({ mensaje: 'Horario eliminado' });
};
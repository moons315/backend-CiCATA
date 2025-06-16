const Equipo = require('../models/equipo');

exports.getEquipos = async (req, res) => {
  const equipos = await Equipo.findAll();
  res.json(equipos);
};

exports.createEquipo = async (req, res) => {
  const equipo = await Equipo.create(req.body);
  res.status(201).json(equipo);
};

exports.updateEquipo = async (req, res) => {
  const { id } = req.params;
  await Equipo.update(req.body, { where: { id } });
  res.json({ mensaje: 'Equipo actualizado' });
};

exports.deleteEquipo = async (req, res) => {
  const { id } = req.params;
  await Equipo.destroy({ where: { id } });
  res.json({ mensaje: 'Equipo eliminado' });
};
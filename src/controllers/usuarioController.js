const Usuario = require('../models/usuario');

exports.getUsuarios = async (req, res) => {
  const usuarios = await Usuario.findAll();
  res.json(usuarios);
};

exports.createUsuario = async (req, res) => {
  const usuario = await Usuario.create(req.body);
  res.status(201).json(usuario);
};

exports.updateUsuario = async (req, res) => {
  const { id } = req.params;
  await Usuario.update(req.body, { where: { id } });
  res.json({ mensaje: 'Usuario actualizado' });
};

exports.deleteUsuario = async (req, res) => {
  const { id } = req.params;
  await Usuario.destroy({ where: { id } });
  res.json({ mensaje: 'Usuario eliminado' });
};
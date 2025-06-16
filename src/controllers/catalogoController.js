const Catalogo = require('../models/catalogo');

exports.getCatalogos = async (req, res) => {
  const catalogos = await Catalogo.findAll();
  res.json(catalogos);
};

exports.createCatalogo = async (req, res) => {
  const catalogo = await Catalogo.create(req.body);
  res.status(201).json(catalogo);
};

exports.updateCatalogo = async (req, res) => {
  const { id } = req.params;
  await Catalogo.update(req.body, { where: { id } });
  res.json({ mensaje: 'Catálogo actualizado' });
};

exports.deleteCatalogo = async (req, res) => {
  const { id } = req.params;
  await Catalogo.destroy({ where: { id } });
  res.json({ mensaje: 'Catálogo eliminado' });
};
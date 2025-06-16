const express = require('express');
const router = express.Router();
const catalogoController = require('../controllers/catalogoController');

router.get('/', catalogoController.getCatalogos);
router.post('/', catalogoController.createCatalogo);
router.put('/:id', catalogoController.updateCatalogo);
router.delete('/:id', catalogoController.deleteCatalogo);

module.exports = router;
const express = require('express');
const router = express.Router();
const reporteController = require('../controllers/reporteController');

router.get('/reservas-por-usuario', reporteController.reporteReservasPorUsuario);
router.get('/uso-equipos', reporteController.reporteUsoEquipos);
router.get('/eventos-por-fecha', reporteController.reporteEventosPorFecha);

module.exports = router;
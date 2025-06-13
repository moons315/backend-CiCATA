const Horario = require('../models/horario');

// backend/src/controllers/horarioController.js


// Controlador para obtener horarios disponibles para los equipos y rango de fechas 
exports.getHorariosDisponibles = async (req, res) => {
    const { equipo, fechaInicio, fechaFin } = req.query;

    if (!equipo || !fechaInicio || !fechaFin) {
        return res.status(400).json({ message: 'equipo, fechaInicio y fechaFin son requeridos' });
    }

    try {
        const horariosDisponibles = await Horario.find({
            dia: { $gte: fechaInicio, $lte: fechaFin },
            equipo: equipo
        });

        res.json(horariosDisponibles);
    } catch (error) {
        res.status(500).json({ message: 'Error al obtener los horarios', error });
    }
};

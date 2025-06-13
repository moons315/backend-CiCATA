const Reserva = require('../models/reserva'); 
const Equipo = require('../models/equipo');  



exports.crearReserva = async (req, res) => {
    try {
        const { usuarioId, equipoId, fechaInicio, fechaFin } = req.body;

        // Verificar si el equipo existe
        const equipo = await Equipo.findById(equipoId);
        if (!equipo) {
            return res.status(404).json({ mensaje: 'Equipo no encontrado' });
        }

        const reservaExistente = await Reserva.findOne({
            equipo: equipoId, 
            $or: [
                { fechaInicio: { $lte: fechaFin, $gte: fechaInicio } },
                { fechaFin: { $gte: fechaInicio, $lte: fechaFin } },
                { fechaInicio: { $lte: fechaInicio }, fechaFin: { $gte: fechaFin } }
            ]
        });

        if (reservaExistente) {
            return res.status(400).json({ mensaje: 'El equipo ya está reservado en ese horario' });
        }

      
        const nuevaReserva = new Reserva({
            usuario: usuarioId,
            equipo: equipoId,
            fechaInicio,
            fechaFin
        });

        await nuevaReserva.save();
        res.status(201).json(nuevaReserva);
    } catch (error) {
        res.status(500).json({ mensaje: 'Error al crear la reserva', error });
    }
};


exports.obtenerReservas = async (req, res) => {
    try {
        const reservas = await Reserva.find().populate('usuario equipo');
        res.json(reservas);
    } catch (error) {
        res.status(500).json({ mensaje: 'Error al obtener las reservas', error });
    }
};


exports.cancelarReserva = async (req, res) => {
    try {
        const { id } = req.params;
        const reserva = await Reserva.findByIdAndDelete(id);
        if (!reserva) {
            return res.status(404).json({ mensaje: 'Reserva no encontrada' });
        }
        res.json({ mensaje: 'Reserva cancelada correctamente' });
    } catch (error) {
        res.status(500).json({ mensaje: 'Error al cancelar la reserva', error });
    }
};
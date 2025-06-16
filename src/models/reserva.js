const { DataTypes } = require('sequelize');
const sequelize = require('../config/database');

const Reserva = sequelize.define('Reserva', {
  fecha: { type: DataTypes.DATEONLY, allowNull: false },
  horaInicio: { type: DataTypes.STRING }, // opcional
  horaFin: { type: DataTypes.STRING },    // opcional
  tipoReserva: { type: DataTypes.STRING, allowNull: false }, // 'dia' o 'horas'
  estado: { type: DataTypes.STRING, defaultValue: 'pendiente' }
}, { timestamps: true });

module.exports = Reserva;
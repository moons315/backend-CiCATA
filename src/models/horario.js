const { DataTypes } = require('sequelize');
const sequelize = require('../config/database');

const Horario = sequelize.define('Horario', {
  dia: { type: DataTypes.STRING, allowNull: false }, // Ej: 'Lunes'
  horaInicio: { type: DataTypes.STRING, allowNull: false }, // Ej: '08:00'
  horaFin: { type: DataTypes.STRING, allowNull: false },    // Ej: '12:00'
  equipoId: { type: DataTypes.INTEGER, allowNull: false }   // Relaciona con un equipo
}, { timestamps: true });

module.exports = Horario;
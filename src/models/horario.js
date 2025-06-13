const { DataTypes } = require('sequelize');
const sequelize = require('../config/database'); // Ajusta la ruta si es necesario

const Horario = sequelize.define('Horario', {
    dia: {
        type: DataTypes.STRING,
        allowNull: false
    },
    horaInicio: {
        type: DataTypes.STRING,
        allowNull: false
    },
    horaFin: {
        type: DataTypes.STRING,
        allowNull: false
    }
}, {
    timestamps: true
});

module.exports = Horario;

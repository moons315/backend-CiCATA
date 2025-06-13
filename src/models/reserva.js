const { DataTypes } = require('sequelize');
const sequelize = require('../config/database'); // aun no tenemos una base de datos definida para usar jsjs

const Reserva = sequelize.define('Reserva', {
    id: {
        type: DataTypes.INTEGER,
        primaryKey: true,
        autoIncrement: true,
    },
    fecha: {
        type: DataTypes.DATE,
        allowNull: false,
    },
    usuarioId: {
        type: DataTypes.INTEGER,
        allowNull: false,
    },
    recursoId: {
        type: DataTypes.INTEGER,
        allowNull: false,
    },
    estado: {
        type: DataTypes.STRING,
        allowNull: false,
        defaultValue: 'pendiente',
    },
}, {
    tableName: 'reservas',
    timestamps: true,
});

module.exports = Reserva;


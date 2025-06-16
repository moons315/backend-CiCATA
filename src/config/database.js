const { Sequelize } = require('sequelize');

const sequelize = new Sequelize(
  'laboratorio_db',    // Nombre de la base de datos
  'root',              // Usuario de la base de datos
  '1234',     // Contraseña del usuario
  {
    host: 'localhost', // Host de la base de datos
    dialect: 'mysql',  // Cambia a 'postgres', 'sqlite', etc. si usas otro motor
    logging: false,
  }
);

module.exports = sequelize;
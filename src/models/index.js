const Usuario = require('./usuario');
const Equipo = require('./equipo');
const Reserva = require('./reserva');
const Evento = require('./evento');

// Relaciones
Usuario.hasMany(Reserva);
Reserva.belongsTo(Usuario);

Equipo.hasMany(Reserva);
Reserva.belongsTo(Equipo);

module.exports = { Usuario, Equipo, Reserva, Evento };
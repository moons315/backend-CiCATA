const express = require('express');
const cors = require('cors');

// Importa las rutas reales
const usuarioRoutes = require('./routes/usuarioRoutes');
const equipoRoutes = require('./routes/equipoRoutes');
const reservaRoutes = require('./routes/reservaRoutes');
const eventoRoutes = require('./routes/eventoRoutes');
const horarioRoutes = require('./routes/horarioRoutes');
const catalogoRoutes = require('./routes/catalogoRoutes');
const reporteRoutes = require('./routes/reporteRoutes');

const app = express();
app.use(cors());
app.use(express.json());

// Ruta de prueba
app.get('/', (req, res) => {
  res.send('API funcionando ✅');
});

// Usa las rutas reales
app.use('/api/usuarios', usuarioRoutes);
app.use('/api/equipos', equipoRoutes);
app.use('/api/reservas', reservaRoutes);
app.use('/api/eventos', eventoRoutes);
app.use('/api/horarios', horarioRoutes);
app.use('/api/catalogos', catalogoRoutes);
app.use('/api/reportes', reporteRoutes);

module.exports = app;
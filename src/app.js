const express = require('express');
const app = express();


// Middlewares
app.use(express.json());

// Rutas base de ejemplo
app.get('/', (req, res) => {
  res.json({ message: 'FAB LAB Backend funcionando 🚀' });
});

// Aquí puedes agregar más rutas importando desde src/routes

module.exports = app;


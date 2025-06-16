require('dotenv').config();
const app = require('./app');
const sequelize = require('./config/database'); // Ajusta la ruta si es necesario

const PORT = process.env.PORT || 3001;

sequelize.sync().then(() => {
  app.listen(PORT, () => {
    console.log(`Servidor escuchando en http://localhost:${PORT}`);
  });
}).catch((error) => {
  console.error('Error al conectar con la base de datos:', error);
});
-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 17-06-2025 a las 20:11:17
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `turnerolab`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `devices`
--

CREATE TABLE `devices` (
  `Id` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `ProcessId` int(11) NOT NULL,
  `LabId` int(11) NOT NULL,
  `DurationMinutes` int(11) NOT NULL,
  `ConfigJson` text NOT NULL DEFAULT '{}'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `holidays`
--

CREATE TABLE `holidays` (
  `Id` int(11) NOT NULL,
  `Date` date NOT NULL,
  `Description` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `labs`
--

CREATE TABLE `labs` (
  `Id` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Location` varchar(200) NOT NULL,
  `Description` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `processes`
--

CREATE TABLE `processes` (
  `Id` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `LabId` int(11) NOT NULL,
  `DefaultDurationMinutes` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `reservationextensions`
--

CREATE TABLE `reservationextensions` (
  `Id` int(11) NOT NULL,
  `ReservationId` int(11) NOT NULL,
  `RequestedAt` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `NewEndTime` datetime(6) NOT NULL,
  `Approved` tinyint(1) NOT NULL,
  `Observations` varchar(1000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `reservations`
--

CREATE TABLE `reservations` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `DeviceId` int(11) NOT NULL,
  `StartTime` datetime(6) NOT NULL,
  `EndTime` datetime(6) NOT NULL,
  `Status` varchar(20) NOT NULL,
  `Observations` varchar(1000) NOT NULL,
  `CreatedAt` datetime(6) NOT NULL DEFAULT current_timestamp(6)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `roles`
--

CREATE TABLE `roles` (
  `Id` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `users`
--

CREATE TABLE `users` (
  `Id` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `PasswordHash` varchar(200) NOT NULL,
  `RoleId` int(11) NOT NULL,
  `CreatedAt` datetime(6) NOT NULL DEFAULT current_timestamp(6)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20250617151706_InitialCreate', '8.0.13');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `devices`
--
ALTER TABLE `devices`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Devices_LabId` (`LabId`),
  ADD KEY `IX_Devices_ProcessId` (`ProcessId`);

--
-- Indices de la tabla `holidays`
--
ALTER TABLE `holidays`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `labs`
--
ALTER TABLE `labs`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `processes`
--
ALTER TABLE `processes`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Processes_LabId` (`LabId`);

--
-- Indices de la tabla `reservationextensions`
--
ALTER TABLE `reservationextensions`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_ReservationExtensions_ReservationId` (`ReservationId`);

--
-- Indices de la tabla `reservations`
--
ALTER TABLE `reservations`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Reservations_Device_Time` (`DeviceId`,`StartTime`,`EndTime`),
  ADD KEY `IX_Reservations_UserId` (`UserId`);

--
-- Indices de la tabla `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Users_RoleId` (`RoleId`);

--
-- Indices de la tabla `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `devices`
--
ALTER TABLE `devices`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `holidays`
--
ALTER TABLE `holidays`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `labs`
--
ALTER TABLE `labs`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `processes`
--
ALTER TABLE `processes`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `reservationextensions`
--
ALTER TABLE `reservationextensions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `reservations`
--
ALTER TABLE `reservations`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `roles`
--
ALTER TABLE `roles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `users`
--
ALTER TABLE `users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `devices`
--
ALTER TABLE `devices`
  ADD CONSTRAINT `FK_Devices_Labs_LabId` FOREIGN KEY (`LabId`) REFERENCES `labs` (`Id`),
  ADD CONSTRAINT `FK_Devices_Processes_ProcessId` FOREIGN KEY (`ProcessId`) REFERENCES `processes` (`Id`);

--
-- Filtros para la tabla `processes`
--
ALTER TABLE `processes`
  ADD CONSTRAINT `FK_Processes_Labs_LabId` FOREIGN KEY (`LabId`) REFERENCES `labs` (`Id`) ON DELETE CASCADE;

--
-- Filtros para la tabla `reservationextensions`
--
ALTER TABLE `reservationextensions`
  ADD CONSTRAINT `FK_ReservationExtensions_Reservations_ReservationId` FOREIGN KEY (`ReservationId`) REFERENCES `reservations` (`Id`) ON DELETE CASCADE;

--
-- Filtros para la tabla `reservations`
--
ALTER TABLE `reservations`
  ADD CONSTRAINT `FK_Reservations_Devices_DeviceId` FOREIGN KEY (`DeviceId`) REFERENCES `devices` (`Id`),
  ADD CONSTRAINT `FK_Reservations_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`);

--
-- Filtros para la tabla `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `FK_Users_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

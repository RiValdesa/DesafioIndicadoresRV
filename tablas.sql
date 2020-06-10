-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Servidor: localhost
-- Tiempo de generación: 10-06-2020 a las 02:23:34
-- Versión del servidor: 8.0.13-4
-- Versión de PHP: 7.2.24-0ubuntu0.18.04.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `WsqVF47sRQ`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `indicadores`
--

CREATE TABLE `indicadores` (
  `id` int(11) NOT NULL,
  `id_tipo_indicador` int(11) NOT NULL,
  `fecha` date NOT NULL,
  `valor` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;


-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_indicador`
--

CREATE TABLE `tipo_indicador` (
  `id` int(11) NOT NULL,
  `descripcion` varchar(50) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;



--
-- Indices de la tabla `indicadores`
--
ALTER TABLE `indicadores`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_Indicador_tipoIndicador` (`id_tipo_indicador`);

--
-- Indices de la tabla `tipo_indicador`
--
ALTER TABLE `tipo_indicador`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `indicadores`
--
ALTER TABLE `indicadores`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=385;

--
-- AUTO_INCREMENT de la tabla `tipo_indicador`
--
ALTER TABLE `tipo_indicador`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `indicadores`
--
ALTER TABLE `indicadores`
  ADD CONSTRAINT `fk_Indicador_tipoIndicador` FOREIGN KEY (`id_tipo_indicador`) REFERENCES `tipo_indicador` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

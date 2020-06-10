# DesafioIndicadoresRV
- Para este desafío utilice la api de mindicador.cl.
- Aplicativo al iniciar busca el ultimo registro insertado, si el registro es de una fecha anterior, consulta api e inserta valor del día actual que trae la api.
- Se utiliza una base de datos MySql remota de remotemysql.com, la cual ya se encuentra configurada.
- Se usa el ORM Fluent NHibernate para la persistencia.
- Para mostrar la información se utilizó Bootstrap 4, FontAwesome, JQuery y Google Charts.
- Habilitado para navegación Responsive (si se hace desde el navegador, se debe recargar la página)

# Notas
Para modificar valores se debe ingresar mediante el botón de Edición en cada gráfico de columnasAl presionar el botón editar de la tabla se activa un textbox el cual permite el ingreso de valores numéricos y coma, una vez terminada la modificación presionar en botón Guardar.

# Instrucciones 
Clonar o descargar repositorio, ejecutar archivo .sln para iniciar visual studio, compilar la aplicación (para que actualice y descarge los nuggets) y ejecutar con iisExpress
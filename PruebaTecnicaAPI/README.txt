# Instrucciones de Configuración

## Paquete NuGet Local

Este proyecto utiliza un paquete NuGet que ha sido creado de manera local. Para instalar el paquete, sigue los siguientes pasos:

1. **Asegúrate de tener el paquete NuGet disponible en tu máquina**. Si no tienes el paquete local, sigue las instrucciones para crearlo en el proyecto correspondiente.

2. **Agrega la fuente del paquete NuGet**. Abre la consola de NuGet en Visual Studio y ejecuta el siguiente comando para agregar la fuente del paquete:

   ```bash
   nuget sources add -Name "LocalNuGetSource" -Source "C:\PaquetesNuGet"

***********************************************************************************
# Instrucciones para la Base de Datos

## Base de Datos: `PruebDVP`

Este archivo contiene las instrucciones para configurar la base de datos `PruebDVP`. Asegúrate de seguir los pasos a continuación para garantizar que la base de datos esté correctamente configurada.

### Detalles de la Base de Datos

- **Nombre de la base de datos**: `PruebDVP`
- **Usuario inicial para el registro de personas**: `admin`
- **Contraseña de usuario**: `123456`

### Pasos para configurar la base de datos:

1. **Ejecutar el script de creación de base de datos**:
   - El archivo de script para crear la base de datos es `PruebDVP.sql`. 
   - Este script incluye las instrucciones necesarias para crear la base de datos y sus tablas.
   - Abre tu cliente de base de datos (por ejemplo, SQL Server Management Studio, Azure Data Studio, o cualquier otro compatible con tu base de datos) y ejecuta el script `PruebDVP.sql`.

2. **Conectar a la base de datos**:
   - Después de ejecutar el script, asegúrate de que tu aplicación esté conectada correctamente a la base de datos utilizando la cadena de conexión correspondiente en el archivo appsettings.json.
   - Ejemplo de cadena de conexión (ajusta según sea necesario):

     ```json
     "ConnectionStrings": {
       "PT": "Server=localhost;Database=PruebDVP;User Id=USER;Password=123456;"
     }
     ```

3. **Verificación de la base de datos**:
   - Una vez ejecutado el script y configurada la conexión, puedes verificar que la base de datos `PruebDVP` se haya creado correctamente ejecutando la siguiente consulta SQL:

     ```sql
     SELECT name FROM sys.databases WHERE name = 'PruebDVP';
     ```

4. **Problemas comunes**:
   - Si no puedes conectarte a la base de datos, asegúrate de que el servidor de base de datos esté en ejecución y accesible.
   - Si el script SQL no crea la base de datos correctamente, revisa el archivo `PruebDVP.sql` para detectar posibles errores de sintax

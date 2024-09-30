Se una .net 7.0

La aplicación tiene instalados estos paquetes:
AutoMapper 12.0.1
AutoMapper.Extensions.Microsoft.DependencyInjection 12.0.0
Microsoft.AspNetCore.Authentication.JwtBearer 7.0.14
Microsoft.AspNetCore.OpenApi 7.0.11
Microsoft.EntityFrameworkCore7.0.0
Microsoft.EntityFrameworkCore.Design 7.0.0
Microsoft.EntityFrameworkCore.SqlServer 7.0.0
Microsoft.EntityFrameworkCore.Tools 7.0.0
Swashbuckle.AspNetCore 6.5.0

La aplicación de las pruebas tiene instalados:
Moq 4.20.72
xunit 2.4.2

En appsettings.json está la cadena de conexión a la base de datos sql:
ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DoubleVDB;Integrated Security=True;TrustServerCertificate=True;"
  }
Modifique
localhost\\SQLEXPRESS
Por el nombre de su servidor

La aplicación se ejecuta en el puerto 7202, el token se crea teniendo en cuenta esto:
Jwt": {
    //"key": "softwareLion123.@",
    "key": "Bf+Q2/QN8dF7HTN74ON3XjmtFGslfj1p3vlNoklFZ2g=",
    "Issuer": "https://localhost:7202/",
    "Audience": "https://localhost:7202/",
    "Subject": "baseWebApiSubject"
  },
Ponga el número del puerto de su máquina.

La aplicación tiene estas secciones:
Controladores (Controllers): Son responsables de manejar las solicitudes HTTP entrantes y devolver respuestas adecuadas. En el patrón MVC (Model-View-Controller), los controladores coordinan la interacción entre el modelo y la vista. 
En una API, actúan como intermediarios entre las solicitudes del cliente y la lógica de negocio.

DTOs (Data Transfer Objects): Son objetos que se utilizan para transferir datos entre las capas de una aplicación. A menudo, se usan para simplificar los datos que se envían o reciben en las respuestas de una API, 
evitando exponer las entidades del dominio directamente.

Helpers: Son clases o funciones auxiliares que contienen lógica reutilizable o utilidades comunes, como la manipulación de cadenas, formateo de fechas, validación, etc. Ayudan a evitar la duplicación de código en múltiples lugares.

Mapping (AutoMapper): AutoMapper es una biblioteca que permite mapear objetos de un tipo a otro (por ejemplo, de un modelo de dominio a un DTO y viceversa). Simplifica la conversión de datos entre diferentes capas de la aplicación 
sin tener que escribir código manualmente para copiar las propiedades.

Middleware: Son componentes que forman parte del pipeline de procesamiento de solicitudes y respuestas HTTP en una aplicación de .NET Core. Cada middleware puede modificar la solicitud antes de que llegue al 
controlador o modificar la respuesta antes de que se devuelva al cliente. Ejemplos incluyen autenticación, manejo de errores y registro de logs.

Migrations (Migraciones): Son una característica de Entity Framework que permite realizar cambios en la base de datos a lo largo del tiempo de forma controlada. Las migraciones permiten actualizar 
la estructura de la base de datos (crear, modificar o eliminar tablas y columnas) sin perder datos.

Modelos (Models): Representan las entidades o datos del dominio de la aplicación. Estos modelos a menudo corresponden a tablas en la base de datos y encapsulan la lógica relacionada con los datos que representan.

Servicios (Services): Contienen la lógica de negocio de la aplicación. Los servicios interactúan con los modelos y realizan las operaciones que la aplicación necesita, como validaciones, cálculos o consultas a la base de datos. 
Los servicios desacoplan la lógica del negocio de los controladores, lo que facilita la prueba y el mantenimiento del código.

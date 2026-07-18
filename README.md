# RutChile
Librería para el RUT chileno en C#.

Esta librería permite generar, validar, limpiar y formatear los Rol Único Tributario (RUT o RUN) chilenos.

## Características

✅ Validación de RUT con dígito verificador  
✅ Obtención del dígito verificador  
✅ Generación de RUT aleatorios  
✅ Limpieza y formateo de RUT  
✅ Conversión de formatos (con/sin puntos, con/sin DV)  
✅ Separación de RUT en número y dígito verificador  
✅ Compatible con .NET Framework 4.7.2 y .NET Standard 2.0

## Instalación

* Instalar la librería con NuGet (buscar por RutChile)
* O descargar la librería y agregar la dll RutChile.dll a la referencia del proyecto.

```bash
Install-Package RutChile
```

## Uso rápido

```c#
using EFTEC;

// Validar un RUT
var valido = RutChile.ValidarRut("12.345.678-9"); // devuelve true si el rut es válido

// Obtener dígito verificador
var dv = RutChile.ObtenerDV("12345678"); // devuelve "9"

// Generar RUT aleatorio
var rut = RutChile.GeneraRut(); // devuelve ejemplo: "12345678-9"

// Limpiar formato
var limpio = RutChile.LimpiaRut("12.345.678-9"); // devuelve "12345678-9"

// Formatear RUT
var formateado = RutChile.ConvierteTipoRut("12345678-9", 11, true, true); // devuelve "012.345.678-9"

// Separar RUT
var partes = RutChile.SepararRut("12345678-9"); // devuelve ["12345678", "9"]
```

## Tabla de Contenidos

- [RutChile](#rutchile)
  * [Características](#características)
  * [Instalación](#instalación)
  * [Uso rápido](#uso-rápido)
  * [ValidarRut](#validarrut)
  * [ObtenerDV](#obtenerdv)
  * [SepararRut](#separarrut)
  * [GeneraRut](#generarut)
  * [ConvierteTipoRut](#conviertetiporut)
  * [LimpiaRut](#limpiarut)
  * [License](#license)


## ValidarRut

> bool ValidarRut(string rut, bool contieneDV = true)

Valida un RUT y devuelve `true` si el RUT es válido. En caso contrario, devuelve `false`.
El RUT puede estar en mayúscula, minúscula y contener puntos y espacios.

### Parámetros

- **rut**: El RUT a validar (ejemplo: "12.345.678-9" o "12345678-9")
- **contieneDV** (opcional): Indica si el RUT contiene dígito verificador. Por defecto es `true`. Si es `false`, el método agregará automáticamente el DV calculado.

### Ejemplos

```c#
using EFTEC;

// RUT con puntos y guión
bool valido = RutChile.ValidarRut("12.345.678-9");

// RUT con espacios
bool valido2 = RutChile.ValidarRut("   12.345.678-9   ");

// RUT sin formato
bool valido3 = RutChile.ValidarRut("12345678-9");

// RUT en minúscula con K
bool valido4 = RutChile.ValidarRut("15.000.005-k");

// Validar RUT sin DV (se calcula automáticamente)
bool valido5 = RutChile.ValidarRut("12345678", false);
```


## ObtenerDV

> string ObtenerDV(string rut)

Obtiene el dígito verificador de un RUT incompleto.
En caso de error, devuelve un "?".

### Parámetros

- **rut**: El RUT sin dígito verificador. Puede contener espacios, puntos y estar en mayúscula o minúscula.

### Ejemplos

```c#
using EFTEC;

// Obtener DV desde número limpio
string dv = RutChile.ObtenerDV("12345678"); // devuelve "9"

// Obtener DV desde número con formato
string dv2 = RutChile.ObtenerDV("12.345.678"); // devuelve "9"

// RUT pequeño
string dv3 = RutChile.ObtenerDV("1"); // devuelve "9"

// RUT con K
string dv4 = RutChile.ObtenerDV("15.000.005"); // devuelve "K"
```


## SepararRut

> string[] SepararRut(string rut)

Separa un RUT en dos partes: el número y el dígito verificador.
Si el RUT no tiene dígito verificador, la segunda parte estará vacía.
Si el RUT es inválido, devuelve `["", ""]`.

### Parámetros

- **rut**: El RUT a separar (puede estar con o sin formato)

### Ejemplos

```c#
using EFTEC;

// Separar RUT completo
string[] partes = RutChile.SepararRut("12345678-9"); 
// devuelve ["12345678", "9"]

// Separar RUT con formato
string[] partes2 = RutChile.SepararRut("12.345.678-9"); 
// devuelve ["12345678", "9"]

// RUT sin DV
string[] partes3 = RutChile.SepararRut("12345678"); 
// devuelve ["12345678", ""]

// RUT inválido
string[] partes4 = RutChile.SepararRut("12345678-0"); 
// devuelve ["", ""]
```


## GeneraRut

> string GeneraRut(int desde = 1, int hasta = 99999999) 

Genera un RUT al azar con dígito verificador válido.

### Parámetros

- **desde** (opcional): Número inicial del rango. Por defecto es 1.
- **hasta** (opcional): Número final del rango. Por defecto es 99999999.

### Ejemplos

```c#
using EFTEC;

// Generar RUT aleatorio en el rango completo
string rut = RutChile.GeneraRut(); 
// devuelve ejemplo: "12345678-9"

// Generar RUT en rango específico
string rut2 = RutChile.GeneraRut(1000000, 5000000); 
// devuelve ejemplo: "2345678-K"

// Generar RUT pequeño
string rut3 = RutChile.GeneraRut(1, 1000); 
// devuelve ejemplo: "567-8"
```


## ConvierteTipoRut

> string ConvierteTipoRut(string rut, int largo = 0, bool separador = true, bool dv = true)

Convierte un RUT a un formato específico con opciones de largo (ceros a la izquierda), separador de miles y dígito verificador.

### Parámetros

- **rut**: El RUT a convertir (debe incluir el dígito verificador)
- **largo** (opcional): Si es 0, no se especifica largo. Si es mayor que 0, se rellena con ceros a la izquierda hasta alcanzar el largo especificado. Por defecto es 0.
- **separador** (opcional): Si es `true`, incluye puntos como separador de miles. Por defecto es `true`.
- **dv** (opcional): Si es `true`, incluye el dígito verificador. Por defecto es `true`.

### Retorno

Devuelve el RUT formateado según los parámetros especificados, o `null` si el RUT es inválido.

### Ejemplos

```c#
using EFTEC;

string rut = "5.126.663-3";

// Con formato completo (11 caracteres, con puntos y DV)
string r1 = RutChile.ConvierteTipoRut(rut, 11, true, true); 
// devuelve "005.126.663-3"

// Sin formato (solo número y DV)
string r2 = RutChile.ConvierteTipoRut(rut, 0, false, true); 
// devuelve "5126663-3"

// Solo el número con puntos (sin DV)
string r3 = RutChile.ConvierteTipoRut(rut, 0, true, false); 
// devuelve "5.126.663"

// Con largo fijo sin puntos
string r4 = RutChile.ConvierteTipoRut(rut, 9, false, true); 
// devuelve "05126663-3"

rut = "0000001-9";
string r5 = RutChile.ConvierteTipoRut(rut, 11, true, true);  
// devuelve "000.000.001-9"
```


## LimpiaRut

> string LimpiaRut(string rut)

Limpia un RUT eliminando puntos, espacios al comienzo y final, y lo convierte a mayúsculas.

### Parámetros

- **rut**: El RUT a limpiar

### Ejemplos

```c#
using EFTEC;

// RUT con formato
string rut = "5.126.663-3";
string limpio = RutChile.LimpiaRut(rut); 
// devuelve "5126663-3"

// RUT con espacios
string rut2 = "  12.345.678-k  ";
string limpio2 = RutChile.LimpiaRut(rut2); 
// devuelve "12345678-K"

// RUT en minúscula
string rut3 = "15.000.005-k";
string limpio3 = RutChile.LimpiaRut(rut3); 
// devuelve "15000005-K"
```

## Casos de Uso Comunes

### Validación de formularios

```c#
// Validar RUT ingresado por usuario
string rutUsuario = txtRut.Text;
if (RutChile.ValidarRut(rutUsuario))
{
    // RUT válido, continuar con el proceso
    string rutLimpio = RutChile.LimpiaRut(rutUsuario);
    // Guardar en base de datos: rutLimpio
}
else
{
    // Mostrar error al usuario
    MessageBox.Show("RUT inválido");
}
```

### Formateo para mostrar

```c#
// Convertir RUT de base de datos para mostrar con formato bonito
string rutBD = "12345678-9";
string rutFormateado = RutChile.ConvierteTipoRut(rutBD, 0, true, true);
// Mostrar: "12.345.678-9"
```

### Autocompletar dígito verificador

```c#
// Usuario ingresa solo el número
string numeroRut = txtNumero.Text; // "12345678"
string dv = RutChile.ObtenerDV(numeroRut);
string rutCompleto = numeroRut + "-" + dv;
// Resultado: "12345678-9"
```

### Generación de datos de prueba

```c#
// Generar RUT válido para testing
string rutPrueba = RutChile.GeneraRut(10000000, 25000000);
// Usar en pruebas unitarias o datos de ejemplo
```

## License

MIT License.  
RutChile (c) 2019-2026 Jorge Patricio Castro Castillo

[Escuela de Formacion Tecnica Chile SPA](https://www.eftec.cl)

---

## Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un issue o pull request en el repositorio de GitHub.

## Versiones

- **2.0+**: Compatible con .NET Standard 2.0 y .NET Framework 4.7.2
- Incluye validación, generación, formateo y utilidades de RUT chileno

## Soporte

Para reportar bugs o solicitar nuevas características, por favor usa el sistema de issues de GitHub.



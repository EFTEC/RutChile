# RutChile
Librería para el RUT chileno en C#

Uso.
* Instalar la libreria con NUGET (buscar por RutChile)
* O descargar la libreria y agregar la dll RutChile.dll a la referencia del proyecto.
* Y usar como

```c#
// agregar using EFTEC;

var valido=RutChile.ValidarRut(rut) // devuelve true si el rut es valido.
var dv=RutChile.ObtenerDV(rutSinDV) // obtiene el digito verificador de un rut
var rut=RutChile.GeneraRut();

```

## bool ValidarRut(string rut)

Valida un rut y devuelve true si el rut es valido. En otro caso, devuelve False.
El rut puede estar en mayuscula, minuscula y contener puntos.

Ejemplo:

```c#
// agregar using EFTEC;
bool valido=RutChile.ValidarRut("123456789-0");
```

## string ObtenerDV(string rut)

Obtiene el digito verificador de un rut incompleto.
En caso de error, devuelve un "?"
El rut puede contener espacios, puntos y estar en mayuscula y minuscula pero no puede contener un digito verificador.


```c#
// agregar using EFTEC;
string dv=RutChile.ValidarRut("123456789");
```

## string GeneraRut(int desde=1,int hasta=99999999) 

Genera un rut al azar con digito verificador.
El rut generado es de la forma 000000000-0

```c#
// agregar using EFTEC;
string rut=RutChile.GeneraRut(1,99999999); // devuelve un rut ejemplo: 123456789-0
```
# RutChile
Librería para el RUT chileno en C#

Uso.
* Instalar la libreria con NUGET (buscar por RutChile)
* O descargar la libreria y agregar la dll RutChile.dll a la referencia del proyecto.
* Y usar como

```c#
// agregar using EFTEC;
var valido=RutChile.ValidarRut(rut)
```

## bool ValidarRut(string rut)

Valida un rut y devuelve true si el rut es valido. En otro caso, devuelve False.
El rut puede estar en mayuscula, minuscula y contener puntos.

## string ObtenerDV(string rut)

Obtiene el digito verificador de un rut incompleto.
En caso de error, devuelve un "?"

## string GeneraRut(int desde=1,int hasta=99999999) 

Genera un rut al azar con digito verificador.
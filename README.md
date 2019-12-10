# RutChile
Librería para el RUT chileno en C#.

Esta libreria permite generar, validar y limpiar los Rut Unico Tributario (RUT o RUN)


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

## Tabla de Contenidos

- [RutChile](#rutchile)
  * [ValidarRut](#validarrut)
  * [ObtenerDV](#obtenerdv)
  * [GeneraRut](#generarut)
  * [ConvierteTipoRut](#conviertetiporut)
  * [LimpiaRut](#limpiarut)
  * [License](#license)


## ValidarRut

> bool ValidarRut(string rut)

Valida un rut y devuelve true si el rut es valido. En otro caso, devuelve False.
El rut puede estar en mayuscula, minuscula y contener puntos.

Ejemplo:

```c#
// agregar using EFTEC;
bool valido=RutChile.ValidarRut("123456789-0");
```


## ObtenerDV

> string ObtenerDV(string rut)

Obtiene el digito verificador de un rut incompleto.
En caso de error, devuelve un "?"
El rut puede contener espacios, puntos y estar en mayuscula y minuscula pero no puede contener un digito verificador.


```c#
// agregar using EFTEC;
string dv=RutChile.ValidarRut("123456789");
```


## GeneraRut

> string GeneraRut(int desde=1,int hasta=99999999) 

Genera un rut al azar con digito verificador.
El rut generado es de la forma 000000000-0

```c#
// agregar using EFTEC;
string rut=RutChile.GeneraRut(1,99999999); // devuelve un rut ejemplo: 123456789-0
```


## ConvierteTipoRut

> string ConvierteTipoRut(string rut,int largo=0,bool separador=true,bool dv=true)

Convierte un rut en un rut con/sin largo determinado (ceros a la izquierda)
, con/sin separador de miles y con/sin digito verificador.

```c#
rut= "5.126.663-3"
string r1=RutChile.ConvierteTipoRut(rut,11,true,true); // 005.126.663-3
rut= "0000001-9";
string r2=RutChile.ConvierteTipoRut(rut,11,true,true);  // 000.000.001-9
```


## LimpiaRut

> string LimpiaRut(string rut)

Limpia un rut de puntos, espacios al comienzo y final y lo devuelve en mayuscula 
y con el digito verificador

```c#
rut= "5.126.663-3";
string r=RutChile.LimpiaRut(rut); //5126663-3
```

## License
MIT License.
RutChile (c) 2019 Jorge Patricio Castro Castillo

[Escuela de Formacion Tecnica Chile SPA](https://www.eftec.cl)



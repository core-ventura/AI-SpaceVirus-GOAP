# AI-SpaceVirus-GOAP

Crea un proyecto GOAP en el que un conjunto de agentes lleven a cabo una serie de acciones que les permitan alcanzar sus objetivos. La temática es totalmente libre, por ejemplo se puede simular un pueblo con agentes de todo tipo (cocineros, panaderos, granjeros, mineros, herreros, banqueros, vendedores, ladrones, alguaciles, bomberos, ...). Como referencia, un proyecto con 3 tipos de agentes y 6 acciones en total es un aprobado. A partir de ahí,  se calificará en función de la complejidad del mismo. 

![](Gif-SpaceVirus.gif)

He optado por simular un asentamiento espacial tras un aterrizaje forzoso en un planeta desconocido. Nos encontraremos con agente como Tripulantes, Doctores, Soldados y finalmente los Aliens (Zombies).

![](Screenshot_1.PNG)

---

**Las zonas y sus recursos**

Nos encontraremos con seis diferentes zonas:
1) **Lugar del accidente:** donde se pueden recoger *piezas de la nave espacial*.
2) **Estación de comunicaciones:** donde podremos obtener *planos*.
3) **Zona de investigación**: donde se conseguiremos *investigaciones* y donde nuestros tripulantes pueden ser infectados por esporas alienígenas, convirtíendolos en zombies espaciales.
4) **Facilidad de almacenamiento**: módulo donde guardaremos las piezas, planos e investigaciones.
5) **Enfermería**: lugar donde se almacenan los *medipacks*. En principio esta zona se iba a utilizar como zona de recuperación de salud de los tripulantes y soldados, no obstante, dada la complejidad que se fue generando con el resto de agentes, decidí descartar la idea de puntos de salud y ceñirme a un comportamiento más instantáneo.
6) **Armería**: donde se almacenan las armas y los tripulantes pueden armarse para convertirse en soldados.

**Los agentes y sus acciones**


# Das Haus kliensalkalmazás

Alkalmazás a HotCakesen belüli raktárkészlet kezelésére.

## Specifikáció

[Funkcionális terv](https://rf.uni-corvinus.hu/mantis/view.php?id=4311)

[Dizájn terv](https://rf.uni-corvinus.hu/mantis/view.php?id=4310)

## Branching

A `main` branch védett, csak jóváhagyott pull requesttel lehet behúzni rá kódot.

Minden nagyobb feature fejlesztésekor (pl. adatbázis bekötése, UI létrehozása) az alábbi követendő:

1. Feature branch létrehozása értelemszerű néven
2. Feature lefejlesztése, közben rendszeres commitolás
3. Pull request a `main` branchbe
4. Jóváhagyás esetén feature branch törlése

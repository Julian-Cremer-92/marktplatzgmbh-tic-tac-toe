# marktplatzgmbh-tic-tac-toe
Repository für die Testaufgabe im Bewerbungsverfahren mit der Marktplatz GmbH

# TODO:
1. class GameBoard
1.1. winningComninations wegschmeißen -> WinninwinningCombinations müssen hardcoded angepasst werden für bspw größere Spielfelder.
1.2. Generierung des Spielfeldes anhand eines int -> 4 erzuegt eine 4x4-Feld; Gewinnsumme == 4
1.3. Prüdung nicht mehr auf winning Combinations, sondern auf Summe
1.3.1. Jeder eintrag in das spielfeld wird Object (Entry.cs)
1.3.2. Jeder Eintrag prüft das Feld rechts von sich, das feld diagonal-rechts von sich und das Feld unter sich.
1.3.3. Findet ein Eintrag an einer dieser Stellen einen zum eigenen Spielergehörenden eintrag wird die wegsumme +1 addiert.
1.3.4. Die Prüfung des Spielfeldes findet immer oben links beginnend statt!

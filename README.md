# marktplatzgmbh-tic-tac-toe

Repository für die Testaufgabe im Bewerbungsverfahren mit der Marktplatz GmbH

# Improvements

- Um festzulegen, mit welcher Höhe ein spieler gewinnen kann, wurde ein ein winCount in den Konstruktor eingeführt. Es gibt nun zwei Konstruktoren. Wird im Konstruktor keine Gewinnhöhe festgelegt, oder wird eine Höhe oberhalb der maximal möglichen Zellen im generierten spielfeld angegeben, so gilt die Dimension (bei 3x3 ist die Dimension 3 und damit die maximale anzahl Felder, die besetzt werden können in einer Reihe 3) als Gewinnhöhe.
- Der Ansatz der Spielerspezifischen Bit-basierten Listen wurde fallen gelassen und stattdessen durch eine Liste von `Entry`-Objekten ersetzt.
  - `Entry` Objekte kennen ihre eigenen Position auf dem Spielfeld.
  - `Entry` Objekte können genutzt werden um zu erfahren, ob es ein "Nachbar"-Entry gibt.
- Anstatt eines Enums zur Bestimmung der Spieler wurde aufgrund der Inflexibilität dieses Ansatzes auf eine Liste von `Player`-Objekten hinübergewechselt.
  - Die Anzahl der Spieler lässt sich damit frei einstellen.
  - Die `SwitchPlayer()`-Methode schaltet nun immer mit index + 1 durch die liste, modulo der gesamt anzahl der Spieler, damit der letzte Spieler wieder auf den ersten verweist.

# TODOs

Siehe Issues!

# Ideas
## Online Multiplayer
Online Multy-Player kann durch eine Web-Api realisiert werden. Ein Spieler erstellt einen (Nicht-Dedizierter-Multi-Player). Die Web-API stellt Controller mit Funktionen zur verfügung:
- GameController
  - NewGame(int playerCount, int winCount, int dimension): Game
  - JoinGame(string seed)
  - QuitGame(string seed)
  - Set(string seed, int posX, int posY): Game
- PushService / SignalR um Spielerwechsel mitzuteilen und aktiv das spielfeld im Frontend zu aktualisieren.
  - so kann der aktuelle spielzustand (warte auf andere spieler, ein sieler hat gewonnen etc.) proaktiv mitgeteilt werden.

Das Game-Object
```json
// Game
{
  "seed": "string", // ein token / eine guid um das spiel zu identifizieren. Kann geteilt werden um Spiler einzuladen
  "gameField": Entry[]
}
```
Die  im Game Object abgelegten Informationen sind vollständig um dine Darstellung im Frontend zu ermöglichem. Mit Der Set-Methode kann nun ein Spieler, wenn er am Zug ist einen neuen Eintrag erzeugen, indem er angibt in welchem Spiel, als welcher Spieler, er wo einen Haken setzen möchte. Das Frontend würde schon belegte Felder sperren, dennoch würde das BE eine excepetion werfen, die im Frontend darüber asukunft gibt, dass das feld besetzt ist.

Um die Eindeutigkeit der Spieler zu gewährleisten, bzw umzu verhindern, dass ein Spiler sich für einen anderen ausgibt, wird für jedeSession ein Token generiert. Der PlayerContext, ermittelt dann per Attribute an den Methoden durch die Momentane Session, die Spieler-Id. So kann kein Spiler einen anderen Manipulieren.

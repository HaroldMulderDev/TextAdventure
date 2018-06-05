all: run

Zuul:
	@mcs Zuul.cs Game.cs Parser.cs Room.cs Command.cs CommandLibrary.cs

clean:
	@rm -f Zuul.exe

run: Zuul
	@mono Zuul.exe

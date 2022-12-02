CFLAGS = -g

all: helloworld

helloworld: helloworld.o
	g++ -o $@ $^

helloworld.o: helloworld.cpp
	g++ $(CFLAGS) -c -o $@ $<

clean:
	del helloworld helloworld.o


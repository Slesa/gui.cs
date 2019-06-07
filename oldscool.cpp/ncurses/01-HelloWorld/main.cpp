#include <ncurses.h>
#include <locale>

int main() {
	initscr();
	addstr("Goodbye, cruel world!\n");
	move(3, 10);
	addstr("До звидания!\n");
	refresh();

	int ch;
	do {
		ch = getch();
	} while (ch!=27);

	endwin();
	return 0;
}
#include <ncurses.h>

int main() {

	initscr();
	attron(A_BOLD);
	addstr("Twinkle, twinkle little star\n");
	attron(A_BLINK);
	addstr("How I wonder what you are.\n");

	refresh();
	getch();
	endwin();
	return 0;
}
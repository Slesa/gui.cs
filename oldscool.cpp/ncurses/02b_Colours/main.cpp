#include <ncurses.h>

/*
       COLOR_BLACK   0
        COLOR_RED     1
        COLOR_GREEN   2
        COLOR_YELLOW  3
        COLOR_BLUE    4
        COLOR_MAGENTA 5
        COLOR_CYAN    6
        COLOR_WHITE   7
 */
#define NEW_COLOUR 75 // 8*8+8
char colourNames[8][10] = { "Black", "Red", "Green", "Yellow", "Blue", "Magenta", "Cyan", "White" };
void initColours();
int error(const char* msg);

int main() {

	initscr();
	start_color();
	if( !has_colors() ) {
		return error("No colour support");
	}
	if( !can_change_color() ) {
		return error("Cannot change colour");
	}
	initColours();

	auto col=0;
	for(auto i=0; i<2; i++) {
		for(auto a=0; a<8; a++) {
			for(auto b=0; b<8; b++) {
				if(a==b) continue;
				auto attr = COLOR_PAIR(a*8+b);
				if( i>0 ) attr |= A_BOLD;
				attrset(attr);

				//addstr(colourNames[a]); addstr(" on "); addstr(colourNames[b]); addstr("  ");
				printw("%8s on %8s", colourNames[a], colourNames[b]);
				attrset(COLOR_PAIR(0));
				printw("  ");
				if(++col>4) {
					printw("\b\n");
					col = 0;
				}
			}
		}
	}

	attrset(COLOR_PAIR(NEW_COLOUR));
	printw("Self defined  ");
	attrset(COLOR_PAIR(NEW_COLOUR)|A_BOLD);
	printw("Self defined  ");

	refresh();
	while(1) {
		auto x = getch();
		if( x>='0' && x<='9') {
			bkgd(COLOR_PAIR(x-'0') | x);

			refresh();
			continue;
		}
		if(x=='b') {
			beep(); refresh(); continue;
		}
		if(x=='f') {
			flash(); refresh(); continue;
		}
		break;
	}
	endwin();
	return 0;
}

int error(const char* msg) {
	addstr(msg);
	refresh();
	getch();
	endwin();
	return -1;
}


void initColours() {
	for (auto a = 0; a < 8; a++) {
		for (auto b = 0; b < 8; b++) {
			init_pair(a * 8 + b, a, b);
		}
	}

	init_color(NEW_COLOUR, 100, 100, 75);
	init_pair(NEW_COLOUR, NEW_COLOUR, COLOR_BLACK);
}
#ifndef OLDSCOOL_MNU_H
#define OLDSCOOL_MNU_H
#include <string>
#include <vector>
#include "Vio.h"
#include "Win.h"

using namespace std;

namespace OldScool {
	class MnuEntry {

	public:
		MnuEntry();
		MnuEntry(int x, int y, const string &text, int hotkey = 0, const string &status = nullptr);
		int getX() const { return _x; }
		int getY() const { return _y; }
		int getHotkey() const { return _hotkey; }
		string getText() const { return _text; }
		string getStatus() const { return _status; }

	private:
		int _x;
		int _y;
		int _hotkey;
		string _text;
		string _status;
	};

	class Mnu {

	public:
		Mnu(Vio& vio);
		~Mnu();
		void add(MnuEntry* entry);
		void add(const string& text, const string& status=nullptr);
		void add(const string& text, int hotkey=0, const string& status=nullptr);
		void add(int x, int y, const string& text, int hotkey=0, const string& status=nullptr);
		int handle(const string& title, const vector<int>& exitCodes=vector<int>());
		int handle(const string& title, int x, int y, const vector<int>& exitCodes=vector<int>());

	private:
		Win* getWindow(int x, int y, const string& title);

	private:
		Vio& _vio;
		int _current;
		int _maxWidth;
		int _maxHeight;
		vector<MnuEntry*> _entries;
		Win* _win;
	};
}

using namespace OldScool;

#endif //OLDSCOOL_MNU_H

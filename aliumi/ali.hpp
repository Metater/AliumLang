#include <string>
#include "alidata.hpp"

using namespace std;

class ali
{
    public:
        ali(string sourcePath);
        void load();
        void interpret();

    private:
        alidata data;

        long next;
        char *buffer;
        long filelen;
        void read(string sourcePath);

        char getchar(long pos);
        char* getchars(long pos, int amt);
        char nextchar();
        char* nextchars(int amt);
        char takechar();
        char* takechars(int amt);
        void advance(int amt);
        void advance();
        void jump(long pos);
        bool done();

        short getshort(char* buf);
        int getint(char* buf);
        long getlong(char* buf);

        void eatfuncdecl();
};
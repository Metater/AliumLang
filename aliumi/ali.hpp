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
        void getchars(char* c, long pos, int amt);
        char nextchar();
        void nextchars(char* c, int amt);
        char takechar();
        void takechars(char* c, int amt);
        void advance(int amt);
        void advance();
        void jump(long pos);
        bool done();

        long takelong();

        void eatfuncdecl();
};
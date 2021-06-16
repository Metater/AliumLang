#include "ali.hpp"
#include <iostream>

using namespace std;

ali::ali(string sourcePath)
{
    read(sourcePath);

    next = 0;
}
void ali::load()
{
    while (!done())
    {
        char c = takechar();
        switch (c)
        {
            case 0x00:
                eatfuncdecl();
                break;
            default:
                break;
        }
    }
}
void ali::interpret()
{
    while (!done())
    {
        char c = takechar();
        switch (c)
        {
            case 0x00:
                cout << (int)takechar() << endl;
                break;
            default:
                break;
        }
    }
}



void ali::read(string sourcePath)
{
    FILE *fileptr;

    fileptr = fopen(sourcePath.c_str(), "rb");
    fseek(fileptr, 0, SEEK_END);
    filelen = ftell(fileptr);
    rewind(fileptr);

    buffer = (char *)malloc(filelen * sizeof(char));
    fread(buffer, filelen, 1, fileptr);
    fclose(fileptr);
}

char ali::getchar(long pos) { return buffer[pos]; }
char* ali::getchars(long pos, int amt)
{
    char* buf = new char[amt];
    memcpy(&buf, &buffer[pos], amt);
    return buf;
}
char ali::nextchar() { return getchar(next); }
char* ali::nextchars(int amt) { return getchars(next, amt); }
char ali::takechar()
{
    advance(1);
    return getchar(next-1);
}
char* ali::takechars(int amt)
{
    advance(amt);
    return getchars(next-amt, amt);
}
void ali::advance(int amt) { next += amt; }
void ali::advance() { advance(1); }
void ali::jump(long pos) { next = pos; }
bool ali::done() { return next >= filelen; }

short ali::getshort(char buf[]) { return *reinterpret_cast<long*>(buf); }
int ali::getint(char buf[]) { return *reinterpret_cast<int*>(buf); }
long ali::getlong(char buf[]) { return *reinterpret_cast<long*>(buf); }



void ali::eatfuncdecl()
{
    long id = getlong(takechars(4));
    long pos = getlong(takechars(4));
    if (data.funcdecls.find(id) == data.funcdecls.end())
    {
        data.funcdecls.insert({id, pos});
        cout << "Found func decl id: " << id;
        cout << " pos: " << pos << endl;
        cout << "Retrieved: " << data.funcdecls[id] << endl;
    }
    else
        cout << "Duplicate func id found in declarations" << endl;
}

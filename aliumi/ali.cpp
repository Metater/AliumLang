#include "ali.hpp"
#include <iostream>

using namespace std;

ali::ali(string sourcePath)
{
    read(sourcePath);

    cout << "filelen: " << filelen << endl;



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
    //buffer = new char[filelen];
    fread(buffer, filelen, 1, fileptr);
    fclose(fileptr);
}

char ali::getchar(long pos) { return buffer[pos]; }
void ali::getchars(char* c, long pos, int amt)
{
    memcpy(&c, &buffer[pos], amt);
}
char ali::nextchar() { return getchar(next); }
void ali::nextchars(char* c, int amt) { getchars(c, next, amt); }
char ali::takechar()
{
    advance(1);
    return getchar(next-1);
}
void ali::takechars(char* c, int amt)
{
    advance(amt);
    return getchars(c, next-amt, amt);
}
void ali::advance(int amt) { next += amt; }
void ali::advance() { advance(1); }
void ali::jump(long pos) { next = pos; }
bool ali::done() { return next >= filelen; }

long ali::takelong()
{
    char* temp = new char[8];
    takechars(temp, 8);
    for (int i = 0; i < 8; i++) cout << static_cast<unsigned int>(temp[i]) << endl;
    return *reinterpret_cast<unsigned long*>(temp);
}



void ali::eatfuncdecl()
{
    //char* a;
    //for (int i = 0; i < 8; i++) cout << static_cast<unsigned char>(a[i]) << endl;
    cout << next << endl;
    unsigned long id = takelong();
    cout << next << endl;
    unsigned long pos = takelong();
    cout << next << endl;
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

#ifndef _DB_DUMPER_H
#define _DB_DUMPER_H



class DbDumper
{
public:
	DbDumper() {}

	void dumpSymbolTable(OdDbObjectId tableId, STD(ostream) &os);
	void dumpHeader(OdDbDatabase* pDb, OdDbHostAppServices *pHostApp, STD(ostream) &os);
	void dumpLayers(OdDbDatabase* pDb, STD(ostream) &os);
	void dumpBlocks(OdDbDatabase* pDb, STD(ostream) &os);
	void dumpEntity(OdDbObjectId id, STD(ostream) &os);
  void dumpObject(OdDbObjectId id, const OdString& itemName, STD(ostream) &os, const OdString& pad = "");
  void dumpDimAssoc(OdDbObjectPtr pObject, STD(ostream) & os, const OdString& pad);
  void dumpOsnapPointRef(OdDbOsnapPointRefPtr pRef, STD(ostream) & os, const OdString& pad);
  void dumpXrefFullSubentPath(OdDbXrefFullSubentPath& subEntPath, STD(ostream) & os, const OdString& pad, int increment = 0);
  void dumpGroupCodes(OdResBuf* xIter, STD(ostream) &os, const OdString& pad);
  void dumpMLineStyles( OdDbDatabase* pDb , STD(ostream) & os );
	void dump(OdDbDatabase* pDb, OdDbHostAppServices *pHostApp , STD(ostream) &os);
};


#endif


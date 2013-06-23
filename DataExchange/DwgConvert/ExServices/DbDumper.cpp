#include "OdaCommon.h"

#include "DbDatabase.h"
#include "DbFiler.h"

#include "DbLayerTable.h"
#include "DbLayerTableRecord.h"
#include "DbBlockTable.h"
#include "DbBlockTableRecord.h"

#include "DbDictionary.h"
#include "DbXrecord.h"
#include "DbMlineStyle.h"

#include "DbEntity.h"
#include "DbDimAssoc.h"

#include "ExProtocolExtension.h"
#include "ExHostAppServices.h"

#define STL_USING_IOSTREAM
#include "OdaSTL.h"
#define  STD(a)  std:: a

#include "DbDumper.h"


void DbDumper::dumpHeader(OdDbDatabase* pDb, OdDbHostAppServices *pHostApp,STD(ostream) &os)
{
  OdString sName = pDb->getFilename();
  os << "Database was loaded from: " << sName.c_str() << STD(endl);

  OdDb::DwgVersion vVer = pDb->originalFileVersion();
  os << "File version is: " << OdDb::DwgVersionToStr(vVer) << STD(endl);

  os << "Header Variables:" << STD(endl);
  os << "  LTSCALE: " << pDb->getLTSCALE() << STD(endl);
  os << "  ATTMODE: " << pDb->getATTMODE() << STD(endl);

#ifdef  DUMP_ALL_HEADERVARS
  os << "  DIMASO: "        << pDb->getDIMASO()         << STD(endl);               
  os << "  DIMSHO: "        << pDb->getDIMSHO()         << STD(endl);               
  os << "  PLINEGEN: "      << pDb->getPLINEGEN()       << STD(endl);                
  os << "  ORTHOMODE: "     << pDb->getORTHOMODE()      << STD(endl);               
  os << "  REGENMODE: "     << pDb->getREGENMODE()      << STD(endl);               
  os << "  FILLMODE: "      << pDb->getFILLMODE()       << STD(endl);                
  os << "  QTEXTMODE: "     << pDb->getQTEXTMODE()      << STD(endl);               
  os << "  PSLTSCALE: "     << pDb->getPSLTSCALE()      << STD(endl);               
  os << "  LIMCHECK: "      << pDb->getLIMCHECK()       << STD(endl);                
  os << "  USRTIMER: "      << pDb->getUSRTIMER()       << STD(endl);               
  os << "  SKPOLY: "        << pDb->getSKPOLY()         << STD(endl);               
  os << "  ANGDIR: "        << pDb->getANGDIR()         << STD(endl);               
  os << "  SPLFRAME: "      << pDb->getSPLFRAME()       << STD(endl);               
  os << "  MIRRTEXT: "      << pDb->getMIRRTEXT()       << STD(endl);                
  os << "  WORLDVIEW: "     << pDb->getWORLDVIEW()      << STD(endl);               
  os << "  TILEMODE: "      << pDb->getTILEMODE()       << STD(endl);                
  os << "  PLIMCHECK: "     << pDb->getPLIMCHECK()      << STD(endl);               
  os << "  VISRETAIN: "     << pDb->getVISRETAIN()      << STD(endl);               
  os << "  DISPSILH: "      << pDb->getDISPSILH()       << STD(endl);               
  os << "  PELLIPSE: "      << pDb->getPELLIPSE()       << STD(endl);               
  os << "  PROXYGRAPHICS: " << pDb->getPROXYGRAPHICS()  << STD(endl);                
  os << "  TREEDEPTH: "     << pDb->getTREEDEPTH()      << STD(endl);               
  os << "  LUNITS: "        << pDb->getLUNITS()         << STD(endl);               
  os << "  LUPREC: "        << pDb->getLUPREC()         << STD(endl);                   
  os << "  AUNITS: "        << pDb->getAUNITS()         << STD(endl);                                   
  os << "  AUPREC: "        << pDb->getAUPREC()         << STD(endl);                   
  os << "  ATTMODE: "       << pDb->getATTMODE()        << STD(endl);                   
  os << "  PDMODE: "        << pDb->getPDMODE()         << STD(endl);           
  os << "  USERI1: "        << pDb->getUSERI1()         << STD(endl);           
  os << "  USERI2: "        << pDb->getUSERI2()         << STD(endl);           
  os << "  USERI3: "        << pDb->getUSERI3()         << STD(endl);           
  os << "  USERI4: "        << pDb->getUSERI4()         << STD(endl);           
  os << "  USERI5: "        << pDb->getUSERI5()         << STD(endl);           
  os << "  SPLINESEGS: "    << pDb->getSPLINESEGS()     << STD(endl);           
  os << "  SURFU: "         << pDb->getSURFU()          << STD(endl);           
  os << "  SURFV: "         << pDb->getSURFV()          << STD(endl);           
  os << "  SURFTYPE: "      << pDb->getSURFTYPE()       << STD(endl);           
  os << "  SURFTAB1: "      << pDb->getSURFTAB1()       << STD(endl);           
  os << "  SURFTAB2: "      << pDb->getSURFTAB2()       << STD(endl);           
  os << "  SPLINETYPE: "    << pDb->getSPLINETYPE()     << STD(endl);           
  os << "  SHADEDGE: "      << pDb->getSHADEDGE()       << STD(endl);           
  os << "  SHADEDIF: "      << pDb->getSHADEDIF()       << STD(endl);           
  os << "  UNITMODE: "      << pDb->getUNITMODE()       << STD(endl);           
  os << "  MAXACTVP: "      << pDb->getMAXACTVP()       << STD(endl);           
  os << "  ISOLINES: "      << pDb->getISOLINES()       << STD(endl);           
  os << "  CMLJUST: "       << pDb->getCMLJUST()        << STD(endl);               
  os << "  TEXTQLTY: "      << pDb->getTEXTQLTY()       << STD(endl);          
  os << "  LTSCALE: "       << pDb->getLTSCALE()        << STD(endl);           
  os << "  TEXTSIZE: "      << pDb->getTEXTSIZE()       << STD(endl);          
  os << "  TRACEWID: "      << pDb->getTRACEWID()       << STD(endl);       
  os << "  SKETCHINC: "     << pDb->getSKETCHINC()      << STD(endl);       
  os << "  FILLETRAD: "     << pDb->getFILLETRAD()      << STD(endl);       
  os << "  THICKNESS: "     << pDb->getTHICKNESS()      << STD(endl);        
  os << "  ANGBASE: "       << pDb->getANGBASE()        << STD(endl);        
  os << "  PDSIZE: "        << pDb->getPDSIZE()         << STD(endl);             
  os << "  PLINEWID: "      << pDb->getPLINEWID()       << STD(endl);           
  os << "  USERR1: "        << pDb->getUSERR1()         << STD(endl);         
  os << "  USERR2: "        << pDb->getUSERR2()         << STD(endl);         
  os << "  USERR3: "        << pDb->getUSERR3()         << STD(endl);         
  os << "  USERR4: "        << pDb->getUSERR4()         << STD(endl);         
  os << "  USERR5: "        << pDb->getUSERR5()         << STD(endl);         
  os << "  CHAMFERA: "      << pDb->getCHAMFERA()       << STD(endl);           
  os << "  CHAMFERB: "      << pDb->getCHAMFERB()       << STD(endl);         
  os << "  CHAMFERC: "      << pDb->getCHAMFERC()       << STD(endl);         
  os << "  CHAMFERD: "      << pDb->getCHAMFERD ()      << STD(endl);
  os << "  FACETRES: "      << pDb->getFACETRES()       << STD(endl);         
  os << "  CMLSCALE: "      << pDb->getCMLSCALE()       << STD(endl);         
  os << "  CELTSCALE: "     << pDb->getCELTSCALE()      << STD(endl);            
  os << "  ELEVATION: "     << pDb->getELEVATION()      << STD(endl);      

  os << "  UCSNAME: "       << pDb->getUCSNAME()        << STD(endl);      
  os << "  PUCSNAME: "      << pDb->getPUCSNAME()       << STD(endl);         
  os << "  PELEVATION: "    << pDb->getPELEVATION()     << STD(endl);            
  os << "  PINSBASE: "      << pDb->getPINSBASE().x     << "," << pDb->getPINSBASE().y <<  "," << pDb->getPINSBASE().z << STD(endl);           
  os << "  PEXTMIN: "       << pDb->getPEXTMIN().x      << "," << pDb->getPEXTMIN().y  <<  "," << pDb->getPEXTMIN().z << STD(endl);          
  os << "  PEXTMAX: "       << pDb->getPEXTMAX().x      << "," << pDb->getPEXTMAX().y  <<  "," << pDb->getPEXTMAX().z << STD(endl);         
  os << "  PLIMMIN: "       << pDb->getPLIMMIN().x      << "," << pDb->getPLIMMIN().y  <<  "," << STD(endl);         
  os << "  PLIMMAX: "       << pDb->getPLIMMAX().x      << "," << pDb->getPLIMMAX().y  <<  "," << STD(endl);         
  os << "  PUCSORG: "       << pDb->getPUCSORG().x      << "," << pDb->getPUCSORG().y  <<  "," << pDb->getPUCSORG().z << STD(endl);          
  os << "  PUCSXDIR: "      << pDb->getPUCSXDIR().x     << "," << pDb->getPUCSXDIR().y <<  "," << pDb->getPUCSXDIR().z << STD(endl);         
  os << "  PUCSYDIR: "      << pDb->getPUCSYDIR().x     << "," << pDb->getPUCSYDIR().y <<  "," << pDb->getPUCSYDIR().z  << STD(endl);          
  os << "  INSBASE: "       << pDb->getINSBASE().x      << "," << pDb->getINSBASE().y  <<  "," << pDb->getINSBASE().z << STD(endl);         
  os << "  EXTMIN: "        << pDb->getEXTMIN().x       << "," << pDb->getEXTMIN().y   <<  "," << pDb->getEXTMIN().z << STD(endl);         
  os << "  EXTMAX: "        << pDb->getEXTMAX().x       << "," << pDb->getEXTMAX().y   <<  "," << pDb->getEXTMAX().z << STD(endl);          
  os << "  LIMMIN: "        << pDb->getLIMMIN().x       << "," << pDb->getLIMMIN().y   <<  "," << STD(endl);          
  os << "  LIMMAX: "        << pDb->getLIMMAX().x       << "," << pDb->getLIMMAX().y   <<  "," << STD(endl);        
  os << "  UCSORG: "        << pDb->getUCSORG().x       << "," << pDb->getUCSORG().y   <<  "," << pDb->getUCSORG().z  << STD(endl);       
  os << "  UCSXDIR: "       << pDb->getUCSXDIR().x      << "," << pDb->getUCSXDIR().y  <<  "," << pDb->getUCSXDIR().z << STD(endl);      
  os << "  UCSYDIR: "       << pDb->getUCSYDIR().x      << "," << pDb->getUCSYDIR().y  <<  "," << pDb->getUCSYDIR().z << STD(endl);      

  os << "  Dimscale: "      << pDb->dimscale()          << STD(endl);
  os << "  Dimasz  : "      << pDb->dimasz()            << STD(endl);
  os << "  Dimexo  : "      << pDb->dimexo()            << STD(endl);
  os << "  Dimdli  : "      << pDb->dimdli()            << STD(endl); 
  os << "  Dimexe  : "      << pDb->dimexe()            << STD(endl);
  os << "  Dimrnd  : "      << pDb->dimrnd()            << STD(endl); 
  os << "  Dimdle  : "      << pDb->dimdle()            << STD(endl); 
  os << "  Dimtp   : "      << pDb->dimtp()             << STD(endl); 
  os << "  Dimtm   : "      << pDb->dimtm()             << STD(endl); 
  os << "  Dimtxt  : "      << pDb->dimtxt()            << STD(endl); 
  os << "  Dimcen  : "      << pDb->dimcen()            << STD(endl); 
  os << "  Dimtsz  : "      << pDb->dimtsz()            << STD(endl); 
  os << "  Dimaltf : "      << pDb->dimaltf()           << STD(endl); 
  os << "  Dimlfac : "      << pDb->dimlfac()           << STD(endl); 
  os << "  Dimtvp  : "      << pDb->dimtvp()            << STD(endl);   
  os << "  Dimtfac : "      << pDb->dimtfac()           << STD(endl); 
  os << "  Dimgap  : "      << pDb->dimgap()            << STD(endl); 

  os << "  dimadec: "       << pDb->dimadec()           << STD(endl);
  os << "  dimalt: "        << pDb->dimalt()            << STD(endl);
  os << "  dimaltd: "       << pDb->dimaltd()           << STD(endl);
  os << "  dimaltf: "       << pDb->dimaltf()           << STD(endl);
  os << "  dimaltrn: "      << pDb->dimaltrnd()         << STD(endl);
  os << "  dimalttd: "      << pDb->dimalttd()          << STD(endl);
  os << "  dimalttz: "      << pDb->dimalttz()          << STD(endl);
  os << "  dimaltu: "       << pDb->dimaltu()           << STD(endl);
  os << "  dimaltz: "       << pDb->dimaltz()           << STD(endl);
  os << "  dimapost: "      << pDb->dimapost()          << STD(endl);
  os << "  dimasz: "        << pDb->dimasz()            << STD(endl);
  os << "  dimatfit: "      << pDb->dimatfit()          << STD(endl);
  os << "  dimaunit: "      << pDb->dimaunit()          << STD(endl);
  os << "  dimazin: "       << pDb->dimazin()           << STD(endl);
  os << "  dimblk: "        << pDb->dimblk()            << STD(endl); 
  os << "  dimblk1: "       << pDb->dimblk1()           << STD(endl);
  os << "  dimblk2: "       << pDb->dimblk2()           << STD(endl);
  os << "  dimcen: "        << pDb->dimcen()            << STD(endl); 
  os << "  dimclrd: "       << pDb->dimclrd().color()   << STD(endl);
  os << "  dimclre: "       << pDb->dimclre().color()   << STD(endl);
  os << "  dimclrt: "       << pDb->dimclrt().color()   << STD(endl);
  os << "  dimdec: "        << pDb->dimdec()            << STD(endl); 
  os << "  dimdle: "        << pDb->dimdle()            << STD(endl);
  os << "  dimdli: "        << pDb->dimdli()            << STD(endl);
  os << "  dimexe: "        << pDb->dimexe()            << STD(endl);
  os << "  dimdsep: "       << pDb->dimdsep()           << STD(endl); 
  os << "  dimexo: "        << pDb->dimexo()            << STD(endl);
  os << "  dimfrac: "       << pDb->dimfrac()           << STD(endl);
  os << "  dimgap: "        << pDb->dimgap()            << STD(endl); 
  os << "  dimjust: "       << pDb->dimjust()           << STD(endl);
  os << "  dimldrblk: "     << pDb->dimldrblk()         << STD(endl);
  os << "  dimlfac: "       << pDb->dimlfac()           << STD(endl);
  os << "  dimlim: "        << pDb->dimlim()            << STD(endl);
  os << "  dimlunit: "      << pDb->dimlunit()          << STD(endl);
  os << "  dimlwd: "        << pDb->dimlwd()            << STD(endl); 
  os << "  dimlwe: "        << pDb->dimlwe()            << STD(endl); 
  os << "  dimpost: "       << pDb->dimpost()           << STD(endl); 
  os << "  dimrnd: "        << pDb->dimrnd()            << STD(endl); 
  os << "  dimsah: "        << pDb->dimsah()            << STD(endl); 
  os << "  dimscale: "      << pDb->dimscale()          << STD(endl); 
  os << "  dimsd1: "        << pDb->dimsd1()            << STD(endl); 
  os << "  dimsd2: "        << pDb->dimsd2()            << STD(endl); 
  os << "  dimse1: "        << pDb->dimse1()            << STD(endl); 
  os << "  dimse2: "        << pDb->dimse2()            << STD(endl); 
  os << "  dimsoxd: "       << pDb->dimsoxd()           << STD(endl);
  os << "  dimtad: "        << pDb->dimtad()            << STD(endl); 
  os << "  dimtdec: "       << pDb->dimtdec()           << STD(endl); 
  os << "  dimtfac: "       << pDb->dimtfac()           << STD(endl); 
  os << "  dimtih: "        << pDb->dimtih()            << STD(endl);  
  os << "  dimtix: "        << pDb->dimtix()            << STD(endl);  
  os << "  dimtm: "         << pDb->dimtm()             << STD(endl);   
  os << "  dimtmove: "      << pDb->dimtmove()          << STD(endl); 
  os << "  dimtofl: "       << pDb->dimtofl()           << STD(endl); 
  os << "  dimtoh: "        << pDb->dimtoh()            << STD(endl);  
  os << "  dimtol: "        << pDb->dimtol()            << STD(endl); 
  os << "  dimtolj: "       << pDb->dimtolj()           << STD(endl);
  os << "  dimtp: "         << pDb->dimtp()             << STD(endl);  
  os << "  dimtsz: "        << pDb->dimtsz()            << STD(endl);  
  os << "  dimtvp: "        << pDb->dimtvp()            << STD(endl);  
  os << "  dimtxsty: "      << pDb->dimtxsty()          << STD(endl);  
  os << "  dimtxt: "        << pDb->dimtxt()            << STD(endl);   
  os << "  dimtzin: "       << pDb->dimtzin()           << STD(endl); 
  os << "  dimupt: "        << pDb->dimupt()            << STD(endl);  
  os << "  dimzin: "        << pDb->dimzin()            << STD(endl);  

  os << "  ATTREQ: "        << pHostApp->getATTREQ()    << STD(endl);       
  os << "  ATTDIA: "        << pHostApp->getATTDIA()    << STD(endl);        
  os << "  BLIPMODE: "      << pHostApp->getBLIPMODE()  << STD(endl);      
  os << "  DELOBJS: "       << pHostApp->getDELOBJS()   << STD(endl);       
  os << "  COORDS: "        << pHostApp->getCOORDS()    << STD(endl);        
  os << "  DRAGMODE: "      << pHostApp->getDRAGMODE()  << STD(endl);      
  os << "  OSMODE: "        << pHostApp->getOSMODE()    << STD(endl);        
  os << "  PICKSTYLE: "     << pHostApp->getPICKSTYLE() << STD(endl);     
  os << "  LWDEFAULT: "     << pHostApp->getLWDEFAULT() << STD(endl);      
  os << "  FONTALT: "       << pHostApp->getFONTALT()   << STD(endl);       
  os << "  PLINETYPE: "     << pHostApp->getPLINETYPE() << STD(endl);     
  os << "  SAVEROUNDTRIP: " << pHostApp->getSAVEROUNDTRIP()   << STD(endl); 
  os << "  LWDISPSCALE: "   << pHostApp->getLWDISPSCALE()     << STD(endl);   
  os << "  PREVIEW_WIDTH: " << pHostApp->getPREVIEW_WIDTH()   << STD(endl);   
  os << "  PREVIEW_HEIGHT: "<< pHostApp->getPREVIEW_HEIGHT()  << STD(endl); 
  os << "  GRIPHOVER: "     << pHostApp->getGRIPHOVER() << STD(endl);      
  os << "  GRIPOBJLIMIT: "  << pHostApp->getGRIPOBJLIMIT()    << STD(endl);  
  os << "  GRIPTIPS: "      << pHostApp->getGRIPTIPS()  << STD(endl);       
  os << "  HPASSOC: "       << pHostApp->getHPASSOC()   << STD(endl);        
  os << "  LOGFILEMODE: "   << pHostApp->getLOGFILEMODE()     << STD(endl);    
  os << "  LOCALROOTPREFIX: " << pHostApp->getLOCALROOTPREFIX()  << STD(endl); 
  os << "  MAXHATCHDENSITY: " << pHostApp->getMAXHATCHDENSITY()  << STD(endl);
#endif
  OdDbDate d = pDb->getTDCREATE();
  short month, day, year, hour, min, sec, msec;
  d.getDate(month, day, year);
  d.getTime(hour, min, sec, msec);
  os << "  TDCREATE: " << month << "-" << day << "-" << year << ", " << hour << ":" << min << ":" << sec << STD(endl);

  d = pDb->getTDUPDATE();
  d.getDate(month, day, year);
  d.getTime(hour, min, sec, msec);
  os << "  TDUPDATE: " << month << "-" << day << "-" << year << ", " << hour << ":" << min << ":" << sec << STD(endl);

}


void DbDumper::dumpSymbolTable(OdDbObjectId tableId, STD(ostream) & os)
{
  OdDbSymbolTablePtr pTable = tableId.safeOpenObject();
  os << STD(endl) << pTable->isA()->name() << STD(endl);
  OdDbSymbolTableIteratorPtr pIter = pTable->newIterator();
  for (pIter->start(); !pIter->done(); pIter->step())
  {
    OdDbSymbolTableRecordPtr pTableRec = pIter->getRecordId().safeOpenObject();
    os << "  " << pTableRec->getName().c_str() << " <" << 
      pTableRec->isA()->name() << ">" << STD(endl);
  }
}

void DbDumper::dumpLayers(OdDbDatabase* pDb, STD(ostream) & os)
{
  // Layer table smart pointer, opened for read.
  OdDbLayerTablePtr pLayers = pDb->getLayerTableId().safeOpenObject();

  os << STD(endl) << pLayers->desc()->name() << STD(endl);

  // Get a new layer table iterator (as a smart pointer)
  OdDbSymbolTableIteratorPtr pIter = pLayers->newIterator();
  for (pIter->start(); !pIter->done(); pIter->step())
  {
    // Layer Record smart pointer, opened for read.
    OdDbLayerTableRecordPtr pLayer = 
      pIter->getRecordId().safeOpenObject();

    // Read access to the layer record data:
    os << "  " << pLayer->getName().c_str() << " <" << 
      pLayer->desc()->name() << ">";
    os << ", " << (pLayer->isOff() ? "Off" : "On");
    os << ", " << (pLayer->isLocked() ? "Locked" : "Unlocked");
    os << ", " << (pLayer->isDependent() ? 
      "Dep. on XRef" : "Not dep. on XRef");
    os << STD(endl);
  }
} // end DbDumper::dumpLayers

void DbDumper::dumpEntity(OdDbObjectId id, STD(ostream) & os)
{
  // Open the entity
  OdDbEntityPtr pEnt = id.safeOpenObject();

  // Retrieve the registered protocol extension object registered 
  // for this object type.
  OdSmartPtr<OdDbEntity_Dumper> pEntDumper = pEnt;

  pEntDumper->dump(pEnt, os);
  
  dumpGroupCodes(pEnt->xData(), os, "      ");    

  if (!pEnt->extensionDictionary().isNull())
  {
    dumpObject(pEnt->extensionDictionary(), 
      "ACAD_XDICTIONARY", os, "      ");
  }
} // end DbDumper::dumpEntity

void DbDumper::dumpBlocks(OdDbDatabase* pDb, STD(ostream) & os)
{
  // Open the block table
  OdDbBlockTablePtr pBlocks = 
    pDb->getBlockTableId().safeOpenObject();
  
  os << STD(endl) << "Blocks: " << STD(endl);
  // Get an iterator for the block table
  OdDbSymbolTableIteratorPtr pBlkIter = pBlocks->newIterator();
  
  // For each block in the block table
  for (pBlkIter->start(); ! pBlkIter->done(); pBlkIter->step())
  {
    // Open the block
    OdDbBlockTableRecordPtr pBlock = 
      pBlkIter->getRecordId().safeOpenObject();
    
    os << "  " << pBlock->getName().c_str() << STD(endl);

    // Get an entity iterator
    OdDbObjectIteratorPtr pEntIter = pBlock->newIterator();
    
    // For each entity in the block
    for (; !pEntIter->done(); pEntIter->step())
    {
      dumpEntity(pEntIter->objectId(), os);
    }
  }
} // end DbDumper::dumpBlocks

void DbDumper::dumpGroupCodes(OdResBuf* xIter,
                              STD(ostream) & os,
                              const OdString& pad)
{
  for (; xIter != 0; xIter = xIter->next())
  {
    int code = xIter->restype();

    os << pad.c_str() << code << ", ";
    switch (OdDxfCode::_getType(code))
    {
      case OdDxfCode::Name:
      case OdDxfCode::String:
        os << xIter->getString().c_str();
        break;

      case OdDxfCode::Bool:
        os << xIter->getBool();
        break;

      case OdDxfCode::Integer8:
        os << xIter->getInt8();
        break;

      case OdDxfCode::Integer16:
        os << xIter->getInt16();
        break;

      case OdDxfCode::Integer32:
        os << xIter->getInt32();
        break;

      case OdDxfCode::Double:
        os << xIter->getDouble();
        break;

      case OdDxfCode::Angle:
        os << xIter->getDouble();
        break;

      case OdDxfCode::Point:
        {
          OdGePoint3d p = xIter->getPoint3d();
          os << p.x << ", " << p.y << ", " << p.z;
        }
        break;

      case OdDxfCode::BinaryChunk:
        os << "<Binary Data>";
        break;

      case OdDxfCode::Handle:
      case OdDxfCode::LayerName:
        //os << xIter->getHandle();
        os << xIter->getString().c_str();
        break;

      case OdDxfCode::ObjectId:
      case OdDxfCode::SoftPointerId:
      case OdDxfCode::HardPointerId:
      case OdDxfCode::SoftOwnershipId:
      case OdDxfCode::HardOwnershipId:
      {
        OdDbHandle   h = xIter->getHandle();
        os << h.ascii();
        break;
      }
      case OdDxfCode::Unknown:
      default:
        os << "Unknown";
        break;
    }
    os << STD(endl);
  }
}

void DbDumper::dumpXrefFullSubentPath(OdDbXrefFullSubentPath& subEntPath, STD(ostream) & os, const OdString& pad, int increment)
{
  for (OdUInt32 j = 0; j < subEntPath.objectIds().size(); j++)
  {
    os << pad.c_str() << (331 + increment) << ": " << subEntPath.objectIds()[j].getHandle().ascii().c_str() << STD(endl);
  }
  os << pad.c_str() << (73 + increment) << ": " << subEntPath.subentId().type() << STD(endl);
  os << pad.c_str() << (91 + increment) << ": " << subEntPath.subentId().index() << STD(endl);
}

void DbDumper::dumpOsnapPointRef(OdDbOsnapPointRefPtr pRef, STD(ostream) & os, const OdString& pad)
{
  os << pad.c_str() << " 1: " << "AcDbOsnapPointRef" << STD(endl);
  os << pad.c_str() << "72: " << pRef->osnapMode() << STD(endl);

  dumpXrefFullSubentPath(pRef->mainEntity(), os, pad, 0);
  
  os << pad.c_str() << "40: " << pRef->nearOsnap() << STD(endl);
  os << pad.c_str() << "10: " << pRef->osnapPoint().x << ", " << pRef->osnapPoint().y << ", " 
     << pRef->osnapPoint().z << STD(endl);

  dumpXrefFullSubentPath(pRef->intersectEntity(), os, pad, 1);

  if (pRef->lastPointRef())
  {
    os << pad.c_str() << "75: " << 1 << STD(endl);
    dumpOsnapPointRef(pRef->lastPointRef(), os, pad);
  }
  else
  {
    os << pad.c_str() << "75: " << 0 << STD(endl);
  }
}

void DbDumper::dumpDimAssoc(OdDbObjectPtr pObject, 
                            STD(ostream) & os, 
                            const OdString& pad)
{
  OdDbDimAssocPtr pDimAssoc = pObject;
  os << pad.c_str() << "90: " << pDimAssoc->associativityFlag() << STD(endl);
  os << pad.c_str() << "70: " << pDimAssoc->transSpace() << STD(endl);
  os << pad.c_str() << "71: " << pDimAssoc->rotatedDimType() << STD(endl);
  for (int i = 0; i < OdDbDimAssoc::kPointAmount; i++)
  {
    OdDbOsnapPointRefPtr pRef = pDimAssoc->osnapPointRef((OdDbDimAssoc::Associativity)i);
    if (!pRef.isNull())
    {
      dumpOsnapPointRef(pRef, os, pad);
    }      
    else
    {
      break;
    }
  }  
}

void DbDumper::dumpObject(OdDbObjectId id, 
                          const OdString& itemName,
                          STD(ostream) & os, 
                          const OdString& padding)
{
  OdString pad(padding);
  // Open the object
  OdDbObjectPtr pObject = id.safeOpenObject();

  os << pad.c_str();
  if (!itemName.isEmpty())
  {
    os << itemName.c_str() << ", ";
  }

  // Print the object's class name.
  os << "<" << pObject->isA()->name() << ">" << STD(endl);

  // Check for specific object types.
  if (pObject->isKindOf(OdDbDictionary::desc()))
  {
    OdDbDictionaryPtr pDic = pObject;

    // Get a dictionary iterator.
    OdDbDictionaryIteratorPtr pIter = pDic->newIterator();
    pad += "  ";
    for (; !pIter->done(); pIter->next())
    {
      // Dump each item in the dictionary.
      dumpObject(pIter->objectId(), pIter->name(), os, pad);
    }
  }
  else if (pObject->isKindOf(OdDbXrecord::desc()))
  {
    OdDbXrecordPtr pXRec = pObject;
    // Following is broken for objectID's
    dumpGroupCodes(pXRec->rbChain(), os, pad + "  ");
  }
  else if (pObject->isKindOf(OdDbDimAssoc::desc()))
  {
    dumpDimAssoc(pObject, os, pad);
  }
} // end DbDumper::dumpObject

void DbDumper::dumpMLineStyles( OdDbDatabase* pDb , STD(ostream) & os )
{
  OdDbDictionaryPtr pDic = pDb->getMLStyleDictionaryId().safeOpenObject();
  OdDbDictionaryIteratorPtr pIter = pDic->newIterator(); /* OdDbDictionaryIterator */
  
  for (; !pIter->done(); pIter->next())
  {
    OdDbObjectId MLinestyleId = pIter->objectId();
    OdDbMlineStylePtr mp = MLinestyleId.safeOpenObject();/*OdDbMlineStyle*/
    if( !mp.isNull() )
      os << "MLineStyle: " << mp->name().c_str() << STD(endl);
  }
}

void DbDumper::dump(OdDbDatabase* pDb,OdDbHostAppServices *pHostApp, STD(ostream) & os)
{
  dumpHeader(pDb,pHostApp , os);
  dumpLayers(pDb, os);
  
  dumpSymbolTable(pDb->getTextStyleTableId(), os);
  dumpSymbolTable(pDb->getRegAppTableId(), os);
  dumpSymbolTable(pDb->getDimStyleTableId(), os);
  dumpSymbolTable(pDb->getLinetypeTableId(), os);
  dumpSymbolTable(pDb->getViewportTableId(), os);
  dumpSymbolTable(pDb->getViewTableId(), os);
  dumpSymbolTable(pDb->getUCSTableId(), os);
  
  dumpBlocks(pDb, os);

  os << STD(endl) << "Objects:" << STD(endl);
  dumpObject(pDb->getNamedObjectsDictionaryId(), "Named Objects Dictionary", os, "  ");
  dumpMLineStyles( pDb , os );
}


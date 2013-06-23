#ifndef _SharedPtr_h_Included
#define _SharedPtr_h_Included

/** Description:

    {group:Other_Classes} 
*/
template <class T> class OdSharedPtr
{
public:

  OdSharedPtr() : _refCount(0), _reference(0){}
  
  OdSharedPtr(T* obj) : _refCount(0)
  { 
    if ((_reference = obj) != 0)
    {
      _refCount = new int(1);
    }
  }
  
  OdSharedPtr(const OdSharedPtr& other) 
  {
    _reference = other._reference;
    _refCount = other._refCount;
    if (_refCount)
    {
      ++*_refCount;
    }
  }

  ~OdSharedPtr()
  {
    if (_refCount && !--*_refCount)
    {
      delete _reference;
      delete _refCount;
    }
  }

  OdSharedPtr& operator=(const OdSharedPtr &other)
  {
    if (_reference != other._reference)
    {
      if (_refCount && !--*_refCount)
      {
        delete _refCount;
        delete _reference;
      }

      _reference = other._reference;
      _refCount = other._refCount;
      if (_refCount)
      {
        ++*_refCount;
      }
    }
    return *this;
  }

  T* operator->() { return _reference; }

  T* get() { return _reference; }
  
  operator T*() { return _reference; }
  operator const T*() const { return _reference; }
  
  const T* operator->() const { return _reference; }
  
  T& operator*(){ return *_reference; }
  
  const T& operator*() const { return *_reference; }

  bool isNull() const {return _reference == 0;}
  
private: 
  T* _reference;
  int* _refCount;
};

#endif // _SharedPtr_h_Included

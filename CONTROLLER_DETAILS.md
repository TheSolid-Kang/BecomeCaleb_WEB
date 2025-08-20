# Controller Implementation Details

## Overview
This document provides detailed implementation information for all controllers in the BecomeCaleb_WEB application, including method signatures, parameters, return types, and implementation details.

## BaseController<T>

### Class Definition
```csharp
namespace ASPMVC_Practice.Controllers
{
    public class BaseController<T> : Controller
    {
        protected readonly ILogger<T> _logger;

        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
```

### Purpose
- Provides common functionality for all controllers
- Injects logging capability through dependency injection
- Serves as the foundation for application-specific controllers

### Usage Pattern
All controllers inherit from this base class to ensure consistent logging and common functionality across the application.

## HomeController

### Class Definition
```csharp
namespace BecomeCaleb_WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
    }
}
```

### Public Actions

#### Index()
```csharp
public IActionResult Index()
{
    return View();
}
```
- **Purpose:** Displays the home page
- **Route:** `/` or `/Home/Index`
- **HTTP Method:** GET
- **Returns:** Home page view
- **Implementation:** Simple view return without model

#### Privacy()
```csharp
public IActionResult Privacy()
{
    return View();
}
```
- **Purpose:** Displays privacy policy page
- **Route:** `/Home/Privacy`
- **HTTP Method:** GET
- **Returns:** Privacy page view
- **Implementation:** Simple view return without model

#### Error()
```csharp
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
{
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
```
- **Purpose:** Displays error page with request ID
- **Route:** `/Home/Error`
- **HTTP Method:** GET
- **Returns:** Error page view with ErrorViewModel
- **Response Cache:** Disabled (Duration = 0, NoStore = true)
- **Implementation:** Creates ErrorViewModel with current request ID or trace identifier

## HabitController

### Class Definition
```csharp
namespace BecomeCaleb_WEB.Controllers.Caleb
{
    public class HabitController : BaseController<HabitController>
    {
        private readonly CalebHabitContext _context;

        public HabitController(ILogger<HabitController> logger) : base(logger)
        {
            _context = new CalebHabitContext();
        }
    }
}
```

### Dependencies
- `CalebHabitContext _context` - Database context for habit tracking data

### Public Actions

#### Create() - GET
```csharp
public IActionResult Create()
{
    PrepareViewBagItems();
    return View(new HabitOccurrenceViewModel());
}
```
- **Purpose:** Displays form for creating new habit occurrence
- **Route:** `/Caleb/Habit/Create`
- **HTTP Method:** GET
- **Returns:** Create form view with HabitOccurrenceViewModel
- **Implementation:** 
  - Calls `PrepareViewBagItems()` to populate dropdown data
  - Creates new `HabitOccurrenceViewModel` instance
  - Returns view with prepared data

#### Create() - POST
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(HabitOccurrenceViewModel viewModel)
{
    // Navigation Property 관련 ModelState 제거
    foreach (var key in ModelState.Keys.ToList())
    {
        if (key.Contains("Navigation"))
        {
            ModelState.Remove(key);
        }
    }

    if (ModelState.IsValid)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Implementation details...
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    return View(viewModel);
}
```
- **Purpose:** Saves new habit occurrence record
- **Route:** `/Caleb/Habit/Create`
- **HTTP Method:** POST
- **Parameters:** `HabitOccurrenceViewModel viewModel`
- **Returns:** Redirect or view with validation errors
- **Features:**
  - Anti-forgery token validation
  - Navigation property cleanup
  - Transaction-based data saving
  - Error handling with rollback

### Private Methods

#### PrepareViewBagItems()
```csharp
private void PrepareViewBagItems()
{
    ViewBag._TCHHabits = new SelectList(
        _context._TCHHabits
            .Where(h => h.IsDeleted == false || h.IsDeleted == null)
            .OrderBy(h => h.HabitName),
        "HabitSeq",
        "HabitName"
    );

    ViewBag._TCHLocations = new SelectList(
        _context._TCHLocations
            .Where(l => l.IsDeleted == false || l.IsDeleted == null)
            .OrderBy(l => l.LocationName),
        "LocationSeq",
        "LocationName"
    );

    ViewBag._TCHStates = new SelectList(
        _context._TCHStates
            .Where(s => s.IsDeleted == false || s.IsDeleted == null)
            .OrderBy(s => s.StateName),
        "StateSeq",
        "StateName"
    );

    ViewBag._TCHActions = new SelectList(
        _context._TCHActions
            .Where(a => a.IsDeleted == false || a.IsDeleted == null)
            .OrderBy(a => a.ActionName),
        "ActionSeq",
        "ActionName"
    );

    ViewBag._TCHCountermeasures = new SelectList(
        _context._TCHCountermeasures
            .Where(c => c.IsDeleted == false || c.IsDeleted == null)
            .OrderBy(c => c.CountermeasureName),
        "CountermeasureSeq",
        "CountermeasureName"
    );
}
```
- **Purpose:** Prepares dropdown data for views
- **Implementation:** 
  - Queries active (non-deleted) items from each master table
  - Creates SelectList objects for dropdown binding
  - Orders items by name for user-friendly display
- **ViewBag Properties:**
  - `_TCHHabits` - Active habits
  - `_TCHLocations` - Active locations
  - `_TCHStates` - Active states
  - `_TCHActions` - Active actions
  - `_TCHCountermeasures` - Active countermeasures

## FinancialLedgerController

### Class Definition
```csharp
namespace BecomeCaleb_WEB.Controllers.Caleb
{
    public class FinancialLedgerController : BaseController<FinancialLedgerController>
    {
        public FinancialLedgerController(ILogger<FinancialLedgerController> logger) : base(logger)
        {
            _context = new CalebContext();
            _TCMinors = _context._TCMinors.ToList();
            _TCMinors = _TCMinors.FindAll(_e => _e.MajorSeq == 4);// 4 == 가계부유형
        }

        private readonly CalebContext _context;
        private readonly List<_TCMinor> _TCMinors;
    }
}
```

### Dependencies
- `CalebContext _context` - Main database context
- `List<_TCMinor> _TCMinors` - Financial categories (filtered by MajorSeq = 4)

### Public Actions

#### Index()
```csharp
public async Task<IActionResult> Index()
{
    return _context._TCFinancialLedgers != null ?
                View(await _context._TCFinancialLedgers.ToListAsync()) :
                Problem("Entity set 'CalebContext._TCFinancialLedgers'  is null.");
}
```
- **Purpose:** Displays list of financial ledger entries
- **Route:** `/Caleb/FinancialLedger/Index`
- **HTTP Method:** GET
- **Returns:** List view of financial ledgers or Problem result
- **Implementation:** 
  - Checks if DbSet is available
  - Returns Problem if DbSet is null
  - Otherwise returns view with all financial ledgers

#### MonthlyUsageAmount(List<string> CategoryMirs)
```csharp
public async Task<IActionResult> MonthlyUsageAmount(List<string>? CategoryMirs)
{
    TempData["_TCMinors"] = _TCMinors;
    TempData["ChartDatas"] = "";
    using (var dbMgr = new MSSQL_Mgr())
    {
        var list = dbMgr.SelectList<_VCategoryUsePrice>(DbMgr.DB_CONNECTION.CALEB, "EXEC SP_GetMonthlyUsageAmount");
        list = GetMonthlyUsage(list);
        if (0 != CategoryMirs.Count)
        {
            var iterable = from _item in CategoryMirs select _item;
            string data = iterable.Aggregate((item1, item2) => item1 += $", {item2}");
            var searchKeywords = SplitSearchKeywords(data, ",");
            CreateChartData(list, searchKeywords);
        }
        return View(list);
    }
}
```
- **Purpose:** Displays monthly financial usage by category
- **Route:** `/Caleb/FinancialLedger/MonthlyUsageAmount`
- **HTTP Method:** GET
- **Parameters:** `List<string> CategoryMirs` - Category minor codes
- **Returns:** Monthly usage view with chart data
- **Implementation:**
  - Stores category data in TempData
  - Executes stored procedure `SP_GetMonthlyUsageAmount`
  - Processes monthly usage data
  - Creates chart data if categories are specified
  - Returns view with processed data

### Private Methods

#### SplitSearchKeywords(string _str, string _delimiter)
```csharp
private List<string> SplitSearchKeywords(string _str, string _delimiter)
{
    List<string> _TCDiaries = new();
    if (_str.Contains(","))
    {
        foreach (var _keyword in _str.Split(","))
        {
            if (_keyword.Equals(""))
                continue;
            var keyword = _keyword.Trim();
            _TCDiaries.Add(keyword);
        }
    }
    else
    {
        var keyword = _str.Trim();
        _TCDiaries.Add(keyword);
    }
    return _TCDiaries;
}
```
- **Purpose:** Splits comma-separated search keywords
- **Parameters:** 
  - `string _str` - Input string to split
  - `string _delimiter` - Delimiter character (comma)
- **Returns:** `List<string>` - Cleaned keyword list
- **Implementation:** 
  - Handles comma-separated values
  - Trims whitespace from each keyword
  - Skips empty keywords
  - Returns single keyword if no comma found

#### GetMonthlyUsage(List<_VCategoryUsePrice> _list)
```csharp
private List<_VCategoryUsePrice> GetMonthlyUsage(List<_VCategoryUsePrice> _list)
{
    Dictionary<int, HashSet<int>> map = new Dictionary<int, HashSet<int>>();
    foreach (_VCategoryUsePrice _item in _list)
    {
        if (false == map.ContainsKey((int)_item.YYYY))
            map.Add((int)_item.YYYY, new HashSet<int>());

        map[(int)_item.YYYY].Add((int)_item.MM);
    }

    List<_VCategoryUsePrice> list = new List<_VCategoryUsePrice>();
    foreach(var _pair in map)
    {
        int yyyy = _pair.Key;
        HashSet<int> set = _pair.Value;
        foreach(int _mm in set)
        {
            foreach(var _mir in _TCMinors)
            {
                list.Add(new _VCategoryUsePrice() { YYYY = yyyy, MM = _mm, CategoryMir = _mir.MinorSeq, Category = _mir.MinorName, TotPrice = 0});
            }
        }
    }
    
    // Additional processing logic...
    return list;
}
```
- **Purpose:** Processes monthly usage data for charting
- **Parameters:** `List<_VCategoryUsePrice> _list` - Raw monthly data
- **Returns:** `List<_VCategoryUsePrice>` - Processed monthly data
- **Implementation:**
  - Creates year-month mapping from raw data
  - Generates complete year-month-category combinations
  - Sets default values for missing combinations
  - Processes actual usage data

#### CreateChartData(List<_VCategoryUsePrice> list, List<string> searchKeywords)
```csharp
private void CreateChartData(List<_VCategoryUsePrice> list, List<string> searchKeywords)
{
    // Implementation for creating chart visualization data
}
```
- **Purpose:** Creates chart data for visualization
- **Parameters:** 
  - `List<_VCategoryUsePrice> list` - Monthly usage data
  - `List<string> searchKeywords` - Category filter keywords
- **Returns:** void
- **Implementation:** Creates chart data based on filtered categories

## _TCDiaryController

### Class Definition
```csharp
namespace BecomeCaleb_WEB.Controllers.Caleb
{
    public class _TCDiaryController : BaseController<_TCDiaryController>
    {
        private readonly CalebContext _context;

        public _TCDiaryController(ILogger<_TCDiaryController> logger) : base(logger)
        {
            _context = new CalebContext();
        }
    }
}
```

### Dependencies
- `CalebContext _context` - Main database context

### Public Actions

#### Index()
```csharp
public async Task<IActionResult> Index()
{
    return _context._TCDiaries != null ?
                View(await _context._TCDiaries.ToListAsync()) :
                Problem("Entity set 'CalebContext._TCDiaries'  is null.");
}
```
- **Purpose:** Displays list of diary entries
- **Route:** `/Caleb/Diary/Index`
- **HTTP Method:** GET
- **Returns:** List view of diaries or Problem result
- **Implementation:** 
  - Checks if DbSet is available
  - Returns Problem if DbSet is null
  - Otherwise returns view with all diary entries

#### Search(string searchString)
```csharp
public async Task<IActionResult> Search(string searchString)
{
    // Implementation for searching diary entries
    return View(searchResults);
}
```
- **Purpose:** Searches diary entries by content
- **Route:** `/Caleb/Diary/Search`
- **HTTP Method:** GET
- **Parameters:** `string searchString` - Search query
- **Returns:** Search results view
- **Implementation:** Searches diary content based on provided string

## _TFBibleController

### Class Definition
```csharp
namespace BecomeCaleb_WEB.Controllers.TwoMites
{
    public class _TFBibleController : BaseController<_TFBibleController>
    {
        private readonly TwoMitesContext _context;

        public _TFBibleController(ILogger<_TFBibleController> logger) : base(logger)
        {
            _context = new TwoMitesContext();
        }
    }
}
```

### Dependencies
- `TwoMitesContext _context` - Bible database context

### Public Actions

#### Index()
```csharp
public async Task<IActionResult> Index()
{
    return _context._TFBibles != null ? 
                View(await _context._TFBibles.ToListAsync()) :
                Problem("Entity set 'TwoMitesContext._TFBibles'  is null.");
}
```
- **Purpose:** Displays list of Bible entries
- **Route:** `/TwoMites/Bible/Index`
- **HTTP Method:** GET
- **Returns:** List view of Bible entries or Problem result
- **Implementation:** 
  - Checks if DbSet is available
  - Returns Problem if DbSet is null
  - Otherwise returns view with all Bible entries

#### Details(int? id)
```csharp
public async Task<IActionResult> Details(int? id)
{
    if (id == null || _context._TFBibles == null)
    {
        return NotFound();
    }

    var _TFBible = await _context._TFBibles
        .FirstOrDefaultAsync(m => m.BibleSeq == id);
    if (_TFBible == null)
    {
        return NotFound();
    }

    return View(_TFBible);
}
```
- **Purpose:** Displays details of specific Bible entry
- **Route:** `/TwoMites/Bible/Details/{id}`
- **HTTP Method:** GET
- **Parameters:** `int? id` - Bible entry ID
- **Returns:** Details view or NotFound
- **Implementation:**
  - Validates ID parameter
  - Checks if DbSet is available
  - Queries database for specific entry
  - Returns NotFound if entry doesn't exist
  - Otherwise returns details view

#### Create() - GET
```csharp
public IActionResult Create()
{
    return View();
}
```
- **Purpose:** Displays form for creating new Bible entry
- **Route:** `/TwoMites/Bible/Create`
- **HTTP Method:** GET
- **Returns:** Create form view
- **Implementation:** Simple view return without model

#### Create() - POST
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("BibleSeq,Testament,Chapter,Verse,Descript")] _TFBible _TFBible)
{
    if (ModelState.IsValid)
    {
        _context.Add(_TFBible);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    return View(_TFBible);
}
```
- **Purpose:** Saves new Bible entry
- **Route:** `/TwoMites/Bible/Create`
- **HTTP Method:** POST
- **Parameters:** `_TFBible _TFBible` - Bible entry data
- **Returns:** Redirect or view with validation errors
- **Features:** 
  - Anti-forgery token validation
  - Model binding with specific properties
  - ModelState validation
  - Redirect to Index on success
- **Implementation:**
  - Validates model state
  - Adds entry to context
  - Saves changes to database
  - Redirects to Index on success
  - Returns view with model on validation failure

#### Edit(int? id) - GET
```csharp
public async Task<IActionResult> Edit(int? id)
{
    if (id == null || _context._TFBibles == null)
    {
        return NotFound();
    }

    var _TFBible = await _context._TFBibles.FindAsync(id);
    if (_TFBible == null)
    {
        return NotFound();
    }
    return View(_TFBible);
}
```
- **Purpose:** Displays form for editing Bible entry
- **Route:** `/TwoMites/Bible/Edit/{id}`
- **HTTP Method:** GET
- **Parameters:** `int? id` - Bible entry ID
- **Returns:** Edit form view or NotFound
- **Implementation:**
  - Validates ID parameter
  - Checks if DbSet is available
  - Queries database for specific entry
  - Returns NotFound if entry doesn't exist
  - Otherwise returns edit view with populated model

#### Edit(int id, _TFBible _TFBible) - POST
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("BibleSeq,Testament,Chapter,Verse,Descript")] _TFBible _TFBible)
{
    if (id != _TFBible.BibleSeq)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(_TFBible);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_TFBibleExists(_TFBible.BibleSeq))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }
    return View(_TFBible);
}
```
- **Purpose:** Updates existing Bible entry
- **Route:** `/TwoMites/Bible/Edit/{id}`
- **HTTP Method:** POST
- **Parameters:** 
  - `int id` - Bible entry ID
  - `_TFBible _TFBible` - Updated Bible entry data
- **Returns:** Redirect or view with validation errors
- **Features:** 
  - Anti-forgery token validation
  - Model binding with specific properties
  - ModelState validation
  - Concurrency handling
  - Redirect to Index on success
- **Implementation:**
  - Validates ID consistency
  - Validates model state
  - Updates entry in context
  - Handles concurrency exceptions
  - Saves changes to database
  - Redirects to Index on success
  - Returns view with model on validation failure

### Private Methods

#### _TFBibleExists(int id)
```csharp
private bool _TFBibleExists(int id)
{
    return (_context._TFBibles?.Any(e => e.BibleSeq == id)).GetValueOrDefault();
}
```
- **Purpose:** Checks if Bible entry exists
- **Parameters:** `int id` - Bible entry ID
- **Returns:** `bool` - True if entry exists, false otherwise
- **Implementation:** Queries database for entry existence

## Controller Patterns and Best Practices

### 1. Dependency Injection
- All controllers use constructor injection for dependencies
- Logger instances are injected through base controller
- Database contexts are instantiated in constructors

### 2. Async/Await Pattern
- Database operations use async/await for better performance
- Action methods return `Task<IActionResult>` for async operations
- Proper exception handling in async contexts

### 3. Model Validation
- All POST actions validate ModelState
- Anti-forgery token validation on all POST actions
- Proper error handling and user feedback

### 4. Database Operations
- Use of Entity Framework Core for data access
- Transaction management for complex operations
- Proper connection string management

### 5. Error Handling
- Consistent error handling patterns
- Proper HTTP status codes
- User-friendly error messages

### 6. Security
- Anti-forgery token protection
- Input validation and sanitization
- Proper authorization checks (when implemented)

---

*This document provides detailed implementation information for all controllers. For additional details, refer to the source code and the main API documentation.*
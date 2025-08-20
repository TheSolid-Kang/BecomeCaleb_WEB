# BecomeCaleb_WEB API Documentation

## Table of Contents
1. [Overview](#overview)
2. [Architecture](#architecture)
3. [Controllers](#controllers)
4. [Models](#models)
5. [Database Contexts](#database-contexts)
6. [ViewModels](#viewmodels)
7. [Dependencies](#dependencies)
8. [Usage Examples](#usage-examples)

## Overview

BecomeCaleb_WEB is an ASP.NET Core 6.0 web application built with Entity Framework Core. The application provides functionality for personal habit tracking, financial management, diary management, and Bible study features. It follows the MVC (Model-View-Controller) pattern and uses SQL Server as the primary database.

**Target Framework:** .NET 6.0 Windows  
**Database Provider:** SQL Server with Entity Framework Core  
**Architecture Pattern:** MVC with Repository Pattern

## Architecture

The application follows a layered architecture:

```
BecomeCaleb_WEB/
├── Controllers/          # MVC Controllers
├── Models/              # Domain Models and ViewModels
├── Models.CalebTbl/     # Caleb Database Context and Models
├── Models.CalebHabitTbl/ # Habit Tracking Models
├── Models.TwoMitesTbl/  # TwoMites Database Context and Models
├── Views/               # Razor Views
├── wwwroot/            # Static Files
└── Program.cs          # Application Entry Point
```

## Controllers

### BaseController<T>
**Namespace:** `ASPMVC_Practice.Controllers`  
**Inheritance:** `Controller`  
**Purpose:** Base controller providing common functionality for all controllers

#### Public Members
- `protected readonly ILogger<T> _logger` - Logger instance for the controller

#### Constructor
```csharp
public BaseController(ILogger<T> logger)
```

#### Usage
```csharp
public class MyController : BaseController<MyController>
{
    public MyController(ILogger<MyController> logger) : base(logger)
    {
        // Controller initialization
    }
}
```

### HomeController
**Namespace:** `BecomeCaleb_WEB.Controllers`  
**Inheritance:** `Controller`  
**Purpose:** Handles home page and error handling

#### Public Actions

##### Index()
- **HTTP Method:** GET
- **Route:** `/` or `/Home/Index`
- **Purpose:** Displays the home page
- **Returns:** `IActionResult` - Home page view

##### Privacy()
- **HTTP Method:** GET
- **Route:** `/Home/Privacy`
- **Purpose:** Displays privacy policy page
- **Returns:** `IActionResult` - Privacy page view

##### Error()
- **HTTP Method:** GET
- **Route:** `/Home/Error`
- **Purpose:** Displays error page with request ID
- **Returns:** `IActionResult` - Error page view with ErrorViewModel
- **Response Cache:** Disabled (Duration = 0, NoStore = true)

#### Usage Example
```csharp
// Navigate to home page
GET /Home/Index

// Navigate to privacy page
GET /Home/Privacy

// Display error page
GET /Home/Error
```

### HabitController
**Namespace:** `BecomeCaleb_WEB.Controllers.Caleb`  
**Inheritance:** `BaseController<HabitController>`  
**Purpose:** Manages habit tracking functionality

#### Dependencies
- `CalebHabitContext _context` - Database context for habit data

#### Public Actions

##### Create()
- **HTTP Method:** GET
- **Route:** `/Caleb/Habit/Create`
- **Purpose:** Displays form for creating new habit occurrence
- **Returns:** `IActionResult` - Create form view with HabitOccurrenceViewModel

##### Create(HabitOccurrenceViewModel viewModel)
- **HTTP Method:** POST
- **Route:** `/Caleb/Habit/Create`
- **Purpose:** Saves new habit occurrence record
- **Parameters:** `HabitOccurrenceViewModel viewModel`
- **Returns:** `IActionResult` - Redirect or view with validation errors
- **Features:** 
  - Anti-forgery token validation
  - Transaction-based data saving
  - Navigation property cleanup

#### Private Methods

##### PrepareViewBagItems()
- **Purpose:** Prepares dropdown data for views
- **Populates ViewBag with:**
  - `_TCHHabits` - Active habits
  - `_TCHLocations` - Active locations
  - `_TCHStates` - Active states
  - `_TCHActions` - Active actions
  - `_TCHCountermeasures` - Active countermeasures

#### Usage Example
```csharp
// Display create form
GET /Caleb/Habit/Create

// Submit habit occurrence
POST /Caleb/Habit/Create
Content-Type: application/x-www-form-urlencoded
__RequestVerificationToken: [token]
// ... form data
```

### FinancialLedgerController
**Namespace:** `BecomeCaleb_WEB.Controllers.Caleb`  
**Inheritance:** `BaseController<FinancialLedgerController>`  
**Purpose:** Manages financial ledger and reporting

#### Dependencies
- `CalebContext _context` - Main database context
- `List<_TCMinor> _TCMinors` - Financial categories

#### Public Actions

##### Index()
- **HTTP Method:** GET
- **Route:** `/Caleb/FinancialLedger/Index`
- **Purpose:** Displays list of financial ledger entries
- **Returns:** `IActionResult` - List view of financial ledgers

##### MonthlyUsageAmount(List<string> CategoryMirs)
- **HTTP Method:** GET
- **Route:** `/Caleb/FinancialLedger/MonthlyUsageAmount`
- **Purpose:** Displays monthly financial usage by category
- **Parameters:** `List<string> CategoryMirs` - Category minor codes
- **Returns:** `IActionResult` - Monthly usage view with chart data
- **Features:**
  - Executes stored procedure `SP_GetMonthlyUsageAmount`
  - Processes monthly usage data
  - Creates chart data for visualization

#### Private Methods

##### SplitSearchKeywords(string _str, string _delimiter)
- **Purpose:** Splits comma-separated search keywords
- **Returns:** `List<string>` - Cleaned keyword list

##### GetMonthlyUsage(List<_VCategoryUsePrice> _list)
- **Purpose:** Processes monthly usage data for charting
- **Returns:** `List<_VCategoryUsePrice>` - Processed monthly data

##### CreateChartData(List<_VCategoryUsePrice> list, List<string> searchKeywords)
- **Purpose:** Creates chart data for visualization

#### Usage Example
```csharp
// View all financial ledgers
GET /Caleb/FinancialLedger/Index

// View monthly usage for specific categories
GET /Caleb/FinancialLedger/MonthlyUsageAmount?CategoryMirs=1&CategoryMirs=2
```

### _TCDiaryController
**Namespace:** `BecomeCaleb_WEB.Controllers.Caleb`  
**Inheritance:** `BaseController<_TCDiaryController>`  
**Purpose:** Manages diary entries and search functionality

#### Public Actions

##### Index()
- **HTTP Method:** GET
- **Route:** `/Caleb/Diary/Index`
- **Purpose:** Displays list of diary entries
- **Returns:** `IActionResult` - List view of diaries

##### Search(string searchString)
- **HTTP Method:** GET
- **Route:** `/Caleb/Diary/Search`
- **Purpose:** Searches diary entries by content
- **Parameters:** `string searchString` - Search query
- **Returns:** `IActionResult` - Search results view

#### Usage Example
```csharp
// View all diary entries
GET /Caleb/Diary/Index

// Search diary entries
GET /Caleb/Diary/Search?searchString=meeting
```

### _TFBibleController
**Namespace:** `BecomeCaleb_WEB.Controllers.TwoMites`  
**Inheritance:** `BaseController<_TFBibleController>`  
**Purpose:** Manages Bible study content

#### Dependencies
- `TwoMitesContext _context` - Bible database context

#### Public Actions

##### Index()
- **HTTP Method:** GET
- **Route:** `/TwoMites/Bible/Index`
- **Purpose:** Displays list of Bible entries
- **Returns:** `IActionResult` - List view of Bible entries

##### Details(int? id)
- **HTTP Method:** GET
- **Route:** `/TwoMites/Bible/Details/{id}`
- **Purpose:** Displays details of specific Bible entry
- **Parameters:** `int? id` - Bible entry ID
- **Returns:** `IActionResult` - Details view or NotFound

##### Create()
- **HTTP Method:** GET
- **Route:** `/TwoMites/Bible/Create`
- **Purpose:** Displays form for creating new Bible entry
- **Returns:** `IActionResult` - Create form view

##### Create(_TFBible _TFBible)
- **HTTP Method:** POST
- **Route:** `/TwoMites/Bible/Create`
- **Purpose:** Saves new Bible entry
- **Parameters:** `_TFBible _TFBible` - Bible entry data
- **Returns:** `IActionResult` - Redirect or view with validation errors
- **Features:** Anti-forgery token validation

##### Edit(int? id)
- **HTTP Method:** GET
- **Route:** `/TwoMites/Bible/Edit/{id}`
- **Purpose:** Displays form for editing Bible entry
- **Parameters:** `int? id` - Bible entry ID
- **Returns:** `IActionResult` - Edit form view or NotFound

##### Edit(int id, _TFBible _TFBible)
- **HTTP Method:** POST
- **Route:** `/TwoMites/Bible/Edit/{id}`
- **Purpose:** Updates existing Bible entry
- **Parameters:** 
  - `int id` - Bible entry ID
  - `_TFBible _TFBible` - Updated Bible entry data
- **Returns:** `IActionResult` - Redirect or view with validation errors
- **Features:** Anti-forgery token validation

#### Usage Example
```csharp
// View all Bible entries
GET /TwoMites/Bible/Index

// View specific Bible entry
GET /TwoMites/Bible/Details/1

// Create new Bible entry
GET /TwoMites/Bible/Create
POST /TwoMites/Bible/Create
Content-Type: application/x-www-form-urlencoded
__RequestVerificationToken: [token]
BibleSeq=0&Testament=Old&Chapter=1&Verse=1&Descript=In the beginning...

// Edit Bible entry
GET /TwoMites/Bible/Edit/1
POST /TwoMites/Bible/Edit/1
Content-Type: application/x-www-form-urlencoded
__RequestVerificationToken: [token]
BibleSeq=1&Testament=Old&Chapter=1&Verse=1&Descript=Updated description...
```

## Models

### Domain Models

#### Caleb Database Models
**Namespace:** `BecomeCaleb_WEB.Models.CalebTbl`

- `_TCDiary` - Diary entries
- `_TCMajor` - Major categories
- `_TCMinor` - Minor categories
- `_TCMission` - Mission statements
- `_TCSearchRecord` - Search records
- `_TCUser` - User information
- `_TCFinancialLedger` - Financial ledger entries

#### Habit Tracking Models
**Namespace:** `BecomeCaleb_WEB.Models.CalebHabitTbl`

- `_TCHHabit` - Habit definitions
- `_TCHLocation` - Habit locations
- `_TCHState` - Habit states
- `_TCHAction` - Habit actions
- `_TCHCountermeasure` - Habit countermeasures
- `_TCHOccurrenceRecord` - Habit occurrence records
- `_TCHOccurrenceAction` - Actions taken during occurrences
- `_TCHOccurrenceCountermeasure` - Countermeasures applied
- `_TCHOccurrenceState` - State changes during occurrences
- `_TCHCorrelationFactor` - Factors correlated with occurrences

#### TwoMites Database Models
**Namespace:** `BecomeCaleb_WEB.Models.TwoMitesTbl`

- `_TFBible` - Bible study content

### ViewModels
**Namespace:** `BecomeCaleb_WEB.Models.ViewModel.Caleb.Habit`

- `HabitOccurrenceViewModel` - Data structure for habit occurrence creation

## Database Contexts

### CalebContext
**Namespace:** `BecomeCaleb_WEB.Models.CalebTbl`  
**Inheritance:** `DbContext`

#### DbSets
```csharp
public virtual DbSet<_TCDiary> _TCDiaries { get; set; }
public virtual DbSet<_TCMajor> _TCMajors { get; set; }
public virtual DbSet<_TCMinor> _TCMinors { get; set; }
public virtual DbSet<_TCMission> _TCMissions { get; set; }
public virtual DbSet<_TCSearchRecord> _TCSearchRecords { get; set; }
public virtual DbSet<_TCUser> _TCUsers { get; set; }
public virtual DbSet<_TCFinancialLedger> _TCFinancialLedgers { get; set; }
```

#### Connection String
```csharp
"Data Source=localhost;Initial Catalog=Caleb;User ID=sa;Password=qp06910691!;Trust Server Certificate=True;Command Timeout=300"
```

#### Configuration
- Applies entity configurations from `Configurations/` folder
- Configures stored procedures and functions
- Supports partial method customization

### CalebHabitContext
**Namespace:** `BecomeCaleb_WEB.Models.CalebHabitTbl`  
**Inheritance:** `DbContext`

#### DbSets
```csharp
public virtual DbSet<_TCHAction> _TCHActions { get; set; }
public virtual DbSet<_TCHCorrelationFactor> _TCHCorrelationFactors { get; set; }
public virtual DbSet<_TCHCountermeasure> _TCHCountermeasures { get; set; }
public virtual DbSet<_TCHHabit> _TCHHabits { get; set; }
public virtual DbSet<_TCHLocation> _TCHLocations { get; set; }
public virtual DbSet<_TCHOccurrenceAction> _TCHOccurrenceActions { get; set; }
public virtual DbSet<_TCHOccurrenceCountermeasure> _TCHOccurrenceCountermeasures { get; set; }
public virtual DbSet<_TCHOccurrenceRecord> _TCHOccurrenceRecords { get; set; }
public virtual DbSet<_TCHOccurrenceState> _TCHOccurrenceStates { get; set; }
public virtual DbSet<_TCHState> _TCHStates { get; set; }
```

#### Connection String
```csharp
"Data Source=localhost;Failover Partner=Caleb;Initial Catalog=Caleb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Command Timeout=300"
```

#### Entity Configuration
- Configures primary keys and foreign keys
- Sets default values for `IsActive` and `IsDeleted` properties
- Establishes cascade delete relationships

### TwoMitesContext
**Namespace:** `BecomeCaleb_WEB.Models.TwoMitesTbl`  
**Inheritance:** `DbContext`

#### DbSets
```csharp
public virtual DbSet<_TFBible> _TFBibles { get; set; }
```

## Dependencies

### NuGet Packages
- **EntityFramework** (6.4.4) - Legacy Entity Framework
- **Microsoft.EntityFrameworkCore.Design** (6.0.22) - EF Core design-time tools
- **Microsoft.EntityFrameworkCore.Sqlite** (6.0.21) - SQLite provider
- **Microsoft.EntityFrameworkCore.SqlServer** (6.0.23) - SQL Server provider
- **Microsoft.EntityFrameworkCore.Tools** (6.0.22) - EF Core tools
- **Microsoft.VisualStudio.Web.CodeGeneration.Design** (6.0.16) - Code generation

### Project References
- **CSharp-Engine** - Custom engine library containing database management and KMP algorithms

## Usage Examples

### 1. Creating a New Habit Occurrence

```csharp
// 1. Navigate to create form
GET /Caleb/Habit/Create

// 2. Submit form data
POST /Caleb/Habit/Create
Content-Type: application/x-www-form-urlencoded

__RequestVerificationToken: [anti-forgery-token]
HabitSeq: 1
LocationSeq: 2
StateSeq: 3
OccurrenceDate: 2024-01-15
OccurrenceTime: 14:30
Description: "Felt stressed during work meeting"
// ... additional form fields
```

### 2. Viewing Monthly Financial Reports

```csharp
// View monthly usage for all categories
GET /Caleb/FinancialLedger/MonthlyUsageAmount

// View monthly usage for specific categories
GET /Caleb/FinancialLedger/MonthlyUsageAmount?CategoryMirs=1&CategoryMirs=2&CategoryMirs=3
```

### 3. Managing Bible Study Content

```csharp
// View all Bible entries
GET /TwoMites/Bible/Index

// Create new Bible entry
POST /TwoMites/Bible/Create
Content-Type: application/x-www-form-urlencoded

__RequestVerificationToken: [anti-forgery-token]
Testament: "New"
Chapter: "John"
Verse: "3:16"
Descript: "For God so loved the world..."
```

### 4. Diary Management

```csharp
// View all diary entries
GET /Caleb/Diary/Index

// Search diary entries
GET /Caleb/Diary/Search?searchString=meeting
```

## Security Features

- **Anti-forgery Token Validation** - All POST actions include `[ValidateAntiForgeryToken]` attribute
- **Input Validation** - ModelState validation on all form submissions
- **SQL Injection Protection** - Entity Framework Core parameterized queries
- **HTTPS Enforcement** - Application configured to use HTTPS in production

## Performance Considerations

- **Database Connection Pooling** - Entity Framework Core manages connection pooling
- **Lazy Loading** - Navigation properties loaded on demand
- **Transaction Management** - Critical operations wrapped in database transactions
- **Command Timeout** - Database commands configured with 300-second timeout

## Error Handling

- **Global Exception Handler** - Configured in `Program.cs` for production environments
- **ModelState Validation** - Client and server-side validation
- **NotFound Responses** - Proper HTTP 404 responses for missing resources
- **Logging** - Structured logging through ILogger interface

## Development Guidelines

1. **Controller Naming** - Use descriptive names ending with "Controller"
2. **Action Naming** - Use standard CRUD action names (Index, Create, Edit, Delete)
3. **Model Validation** - Always validate ModelState before processing
4. **Transaction Usage** - Use transactions for multi-table operations
5. **Error Logging** - Log errors using the injected ILogger instance
6. **Anti-forgery Protection** - Include `[ValidateAntiForgeryToken]` on all POST actions

## Troubleshooting

### Common Issues

1. **Database Connection Errors**
   - Verify SQL Server is running
   - Check connection string in DbContext
   - Ensure database exists and is accessible

2. **ModelState Validation Errors**
   - Check form field names match ViewModel properties
   - Verify required fields are populated
   - Check data types match expected formats

3. **Anti-forgery Token Errors**
   - Ensure `@Html.AntiForgeryToken()` is included in forms
   - Verify token is being sent with POST requests

4. **Entity Framework Errors**
   - Run `Update-Database` to ensure schema is current
   - Check entity configurations in `Configurations/` folder
   - Verify model properties match database schema

## API Versioning

Currently, the application does not implement explicit API versioning. All endpoints are considered version 1.0. For future versions, consider implementing:

- URL-based versioning (`/api/v1/...`)
- Header-based versioning (`Accept: application/vnd.api+json;version=1`)
- Query parameter versioning (`?version=1`)

## Rate Limiting

The application does not currently implement rate limiting. Consider adding rate limiting for production use:

```csharp
// In Program.cs
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter("GlobalLimiter",
            partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));
});
```

---

*This documentation covers the current state of the BecomeCaleb_WEB application as of the last update. For the most current information, refer to the source code and any additional documentation provided by the development team.*
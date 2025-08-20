# Model Documentation

## Overview
This document provides comprehensive documentation for all models, database contexts, and data structures in the BecomeCaleb_WEB application.

## Database Contexts

### CalebContext
**Namespace:** `BecomeCaleb_WEB.Models.CalebTbl`  
**Inheritance:** `DbContext`  
**Purpose:** Main database context for Caleb application data

#### Class Definition
```csharp
public partial class CalebContext : DbContext
{
    public virtual DbSet<_TCDiary> _TCDiaries { get; set; } = null!;
    public virtual DbSet<_TCMajor> _TCMajors { get; set; } = null!;
    public virtual DbSet<_TCMinor> _TCMinors { get; set; } = null!;
    public virtual DbSet<_TCMission> _TCMissions { get; set; } = null!;
    public virtual DbSet<_TCSearchRecord> _TCSearchRecords { get; set; } = null!;
    public virtual DbSet<_TCUser> _TCUsers { get; set; } = null!;
    public virtual DbSet<_TCFinancialLedger> _TCFinancialLedgers { get; set; } = null!;
}
```

#### Constructors
```csharp
public CalebContext() { }
public CalebContext(DbContextOptions<CalebContext> options) : base(options) { }
```

#### Connection String
```csharp
"Data Source=localhost;Initial Catalog=Caleb;User ID=sa;Password=qp06910691!;Trust Server Certificate=True;Command Timeout=300"
```

#### Configuration
- Applies entity configurations from `Configurations/` folder
- Configures stored procedures and functions
- Supports partial method customization through `OnModelCreatingPartial`

#### DbSets

##### _TCDiaries
- **Type:** `DbSet<_TCDiary>`
- **Purpose:** Stores diary entries
- **Usage:** Personal journal and note-taking

##### _TCMajors
- **Type:** `DbSet<_TCMajor>`
- **Purpose:** Major category classifications
- **Usage:** Top-level categorization system

##### _TCMinors
- **Type:** `DbSet<_TCMinor>`
- **Purpose:** Minor category classifications
- **Usage:** Sub-categorization within major categories

##### _TCMissions
- **Type:** `DbSet<_TCMission>`
- **Purpose:** Mission statements and goals
- **Usage:** Personal and professional mission tracking

##### _TCSearchRecords
- **Type:** `DbSet<_TCSearchRecord>`
- **Purpose:** Search history and records
- **Usage:** Tracking user search patterns

##### _TCUsers
- **Type:** `DbSet<_TCUser>`
- **Purpose:** User information and profiles
- **Usage:** User management and authentication

##### _TCFinancialLedgers
- **Type:** `DbSet<_TCFinancialLedger>`
- **Purpose:** Financial transaction records
- **Usage:** Personal finance tracking and reporting

### CalebHabitContext
**Namespace:** `BecomeCaleb_WEB.Models.CalebHabitTbl`  
**Inheritance:** `DbContext`  
**Purpose:** Database context for habit tracking functionality

#### Class Definition
```csharp
public partial class CalebHabitContext : DbContext
{
    public virtual DbSet<_TCHAction> _TCHActions { get; set; } = null!;
    public virtual DbSet<_TCHCorrelationFactor> _TCHCorrelationFactors { get; set; } = null!;
    public virtual DbSet<_TCHCountermeasure> _TCHCountermeasures { get; set; } = null!;
    public virtual DbSet<_TCHHabit> _TCHHabits { get; set; } = null!;
    public virtual DbSet<_TCHLocation> _TCHLocations { get; set; } = null!;
    public virtual DbSet<_TCHOccurrenceAction> _TCHOccurrenceActions { get; set; } = null!;
    public virtual DbSet<_TCHOccurrenceCountermeasure> _TCHOccurrenceCountermeasures { get; set; } = null!;
    public virtual DbSet<_TCHOccurrenceRecord> _TCHOccurrenceRecords { get; set; } = null!;
    public virtual DbSet<_TCHOccurrenceState> _TCHOccurrenceStates { get; set; } = null!;
    public virtual DbSet<_TCHState> _TCHStates { get; set; } = null!;
}
```

#### Constructors
```csharp
public CalebHabitContext() { }
public CalebHabitContext(DbContextOptions<CalebHabitContext> options) : base(options) { }
```

#### Connection String
```csharp
"Data Source=localhost;Failover Partner=Caleb;Initial Catalog=Caleb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Command Timeout=300"
```

#### Entity Configuration
The context configures entities with specific constraints and relationships:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // _TCHAction configuration
    modelBuilder.Entity<_TCHAction>(entity =>
    {
        entity.HasKey(e => e.ActionSeq);
        entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
        entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
    });

    // _TCHCorrelationFactor configuration
    modelBuilder.Entity<_TCHCorrelationFactor>(entity =>
    {
        entity.HasKey(e => e.FactorSeq);
        entity.HasOne(d => d.OccurrenceSeqNavigation)
            .WithMany(p => p._TCHCorrelationFactors)
            .HasForeignKey(d => d.OccurrenceSeq)
            .OnDelete(DeleteBehavior.Cascade);
    });

    // Additional entity configurations...
}
```

#### DbSets

##### _TCHHabits
- **Type:** `DbSet<_TCHHabit>`
- **Purpose:** Defines habit types and categories
- **Key Properties:** HabitSeq (Primary Key)
- **Default Values:** IsActive = 1, IsDeleted = 0

##### _TCHLocations
- **Type:** `DbSet<_TCHLocation>`
- **Purpose:** Defines locations where habits occur
- **Key Properties:** LocationSeq (Primary Key)
- **Default Values:** IsActive = 1, IsDeleted = 0

##### _TCHStates
- **Type:** `DbSet<_TCHState>`
- **Purpose:** Defines possible states for habit occurrences
- **Key Properties:** StateSeq (Primary Key)
- **Default Values:** IsActive = 1, IsDeleted = 0

##### _TCHActions
- **Type:** `DbSet<_TCHAction>`
- **Purpose:** Defines actions that can be taken during habit occurrences
- **Key Properties:** ActionSeq (Primary Key)
- **Default Values:** IsActive = 1, IsDeleted = 0

##### _TCHCountermeasures
- **Type:** `DbSet<_TCHCountermeasure>`
- **Purpose:** Defines countermeasures for habit management
- **Key Properties:** CountermeasureSeq (Primary Key)
- **Default Values:** IsActive = 1, IsDeleted = 0

##### _TCHOccurrenceRecord
- **Type:** `DbSet<_TCHOccurrenceRecord>`
- **Purpose:** Records actual habit occurrences
- **Key Properties:** OccurrenceSeq (Primary Key)
- **Relationships:** Links to habits, locations, and other entities

##### _TCHOccurrenceAction
- **Type:** `DbSet<_TCHOccurrenceAction>`
- **Purpose:** Records actions taken during specific occurrences
- **Key Properties:** Composite key (OccurrenceSeq, ActionSeq)
- **Relationships:** Links occurrences to actions

##### _TCHOccurrenceCountermeasure
- **Type:** `DbSet<_TCHOccurrenceCountermeasure>`
- **Purpose:** Records countermeasures applied during occurrences
- **Key Properties:** Composite key (OccurrenceSeq, CountermeasureSeq)
- **Relationships:** Links occurrences to countermeasures

##### _TCHOccurrenceState
- **Type:** `DbSet<_TCHOccurrenceState>`
- **Purpose:** Records state changes during occurrences
- **Key Properties:** Composite key (OccurrenceSeq, StateSeq)
- **Relationships:** Links occurrences to states

##### _TCHCorrelationFactor
- **Type:** `DbSet<_TCHCorrelationFactor>`
- **Purpose:** Records factors correlated with habit occurrences
- **Key Properties:** FactorSeq (Primary Key)
- **Relationships:** Links to occurrence records with cascade delete

### TwoMitesContext
**Namespace:** `BecomeCaleb_WEB.Models.TwoMitesTbl`  
**Inheritance:** `DbContext`  
**Purpose:** Database context for TwoMites application data

#### Class Definition
```csharp
public partial class TwoMitesContext : DbContext
{
    public virtual DbSet<_TFBible> _TFBibles { get; set; } = null!;
}
```

#### DbSets

##### _TFBibles
- **Type:** `DbSet<_TFBible>`
- **Purpose:** Stores Bible study content and references
- **Usage:** Bible study and scripture management

## Domain Models

### Caleb Database Models

#### _TCDiary
**Purpose:** Represents individual diary entries

**Key Properties:**
- **DiarySeq** (Primary Key) - Unique identifier for diary entry
- **Content** - Text content of the diary entry
- **CreatedDate** - Date when entry was created
- **ModifiedDate** - Date when entry was last modified
- **UserSeq** - Reference to user who created the entry

**Usage:**
```csharp
// Create new diary entry
var diary = new _TCDiary
{
    Content = "Today was a productive day...",
    CreatedDate = DateTime.Now,
    UserSeq = currentUser.UserSeq
};

// Query diary entries
var entries = await context._TCDiaries
    .Where(d => d.UserSeq == userId)
    .OrderByDescending(d => d.CreatedDate)
    .ToListAsync();
```

#### _TCMajor
**Purpose:** Represents major category classifications

**Key Properties:**
- **MajorSeq** (Primary Key) - Unique identifier for major category
- **MajorName** - Name of the major category
- **Description** - Description of the category
- **IsActive** - Whether the category is active
- **IsDeleted** - Soft delete flag

**Usage:**
```csharp
// Get active major categories
var majors = await context._TCMajors
    .Where(m => m.IsActive && !m.IsDeleted)
    .OrderBy(m => m.MajorName)
    .ToListAsync();
```

#### _TCMinor
**Purpose:** Represents minor category classifications within major categories

**Key Properties:**
- **MinorSeq** (Primary Key) - Unique identifier for minor category
- **MinorName** - Name of the minor category
- **MajorSeq** (Foreign Key) - Reference to parent major category
- **Description** - Description of the category
- **IsActive** - Whether the category is active
- **IsDeleted** - Soft delete flag

**Usage:**
```csharp
// Get minor categories for specific major
var minors = await context._TCMinors
    .Where(m => m.MajorSeq == majorId && m.IsActive && !m.IsDeleted)
    .OrderBy(m => m.MinorName)
    .ToListAsync();
```

#### _TCMission
**Purpose:** Represents mission statements and goals

**Key Properties:**
- **MissionSeq** (Primary Key) - Unique identifier for mission
- **MissionText** - Text content of the mission
- **Category** - Category of the mission
- **Priority** - Priority level of the mission
- **TargetDate** - Target completion date
- **IsCompleted** - Whether the mission is completed

**Usage:**
```csharp
// Get active missions
var missions = await context._TCMissions
    .Where(m => !m.IsCompleted)
    .OrderBy(m => m.Priority)
    .ThenBy(m => m.TargetDate)
    .ToListAsync();
```

#### _TCSearchRecord
**Purpose:** Tracks search history and patterns

**Key Properties:**
- **SearchSeq** (Primary Key) - Unique identifier for search
- **SearchTerm** - Search term used
- **SearchDate** - Date when search was performed
- **UserSeq** - Reference to user who performed search
- **ResultCount** - Number of results returned

**Usage:**
```csharp
// Record new search
var search = new _TCSearchRecord
{
    SearchTerm = "meeting",
    SearchDate = DateTime.Now,
    UserSeq = currentUser.UserSeq,
    ResultCount = results.Count
};

// Get search history
var history = await context._TCSearchRecords
    .Where(s => s.UserSeq == userId)
    .OrderByDescending(s => s.SearchDate)
    .Take(10)
    .ToListAsync();
```

#### _TCUser
**Purpose:** Represents user information and profiles

**Key Properties:**
- **UserSeq** (Primary Key) - Unique identifier for user
- **Username** - User's login username
- **Email** - User's email address
- **FirstName** - User's first name
- **LastName** - User's last name
- **IsActive** - Whether the user account is active
- **CreatedDate** - Date when account was created

**Usage:**
```csharp
// Get user by username
var user = await context._TCUsers
    .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

// Create new user
var newUser = new _TCUser
{
    Username = "john.doe",
    Email = "john.doe@example.com",
    FirstName = "John",
    LastName = "Doe",
    IsActive = true,
    CreatedDate = DateTime.Now
};
```

#### _TCFinancialLedger
**Purpose:** Represents financial transaction records

**Key Properties:**
- **LedgerSeq** (Primary Key) - Unique identifier for transaction
- **TransactionDate** - Date of the transaction
- **Amount** - Transaction amount
- **CategoryMinorSeq** (Foreign Key) - Reference to transaction category
- **Description** - Description of the transaction
- **TransactionType** - Type of transaction (Income/Expense)
- **UserSeq** (Foreign Key) - Reference to user who recorded transaction

**Usage:**
```csharp
// Get transactions for specific month
var transactions = await context._TCFinancialLedgers
    .Where(l => l.TransactionDate.Month == month && 
                l.TransactionDate.Year == year)
    .OrderBy(l => l.TransactionDate)
    .ToListAsync();

// Calculate monthly total
var monthlyTotal = await context._TCFinancialLedgers
    .Where(l => l.TransactionDate.Month == month && 
                l.TransactionDate.Year == year)
    .SumAsync(l => l.Amount);
```

### Habit Tracking Models

#### _TCHHabit
**Purpose:** Defines habit types and categories

**Key Properties:**
- **HabitSeq** (Primary Key) - Unique identifier for habit
- **HabitName** - Name of the habit
- **Description** - Description of the habit
- **Category** - Category of the habit
- **IsActive** - Whether the habit is active
- **IsDeleted** - Soft delete flag

**Usage:**
```csharp
// Get active habits
var habits = await context._TCHHabits
    .Where(h => h.IsActive && !h.IsDeleted)
    .OrderBy(h => h.HabitName)
    .ToListAsync();
```

#### _TCHLocation
**Purpose:** Defines locations where habits occur

**Key Properties:**
- **LocationSeq** (Primary Key) - Unique identifier for location
- **LocationName** - Name of the location
- **Description** - Description of the location
- **IsActive** - Whether the location is active
- **IsDeleted** - Soft delete flag

**Usage:**
```csharp
// Get active locations
var locations = await context._TCHLocations
    .Where(l => l.IsActive && !l.IsDeleted)
    .OrderBy(l => l.LocationName)
    .ToListAsync();
```

#### _TCHState
**Purpose:** Defines possible states for habit occurrences

**Key Properties:**
- **StateSeq** (Primary Key) - Unique identifier for state
- **StateName** - Name of the state
- **Description** - Description of the state
- **IsActive** - Whether the state is active
- **IsDeleted** - Soft delete flag

**Usage:**
```csharp
// Get active states
var states = await context._TCHStates
    .Where(s => s.IsActive && !s.IsDeleted)
    .OrderBy(s => s.StateName)
    .ToListAsync();
```

#### _TCHAction
**Purpose:** Defines actions that can be taken during habit occurrences

**Key Properties:**
- **ActionSeq** (Primary Key) - Unique identifier for action
- **ActionName** - Name of the action
- **Description** - Description of the action
- **IsActive** - Whether the action is active
- **IsDeleted** - Soft delete flag

**Usage:**
```csharp
// Get active actions
var actions = await context._TCHActions
    .Where(a => a.IsActive && !a.IsDeleted)
    .OrderBy(a => a.ActionName)
    .ToListAsync();
```

#### _TCHCountermeasure
**Purpose:** Defines countermeasures for habit management

**Key Properties:**
- **CountermeasureSeq** (Primary Key) - Unique identifier for countermeasure
- **CountermeasureName** - Name of the countermeasure
- **Description** - Description of the countermeasure
- **IsActive** - Whether the countermeasure is active
- **IsDeleted** - Soft delete flag

**Usage:**
```csharp
// Get active countermeasures
var countermeasures = await context._TCHCountermeasures
    .Where(c => c.IsActive && !c.IsDeleted)
    .OrderBy(c => c.CountermeasureName)
    .ToListAsync();
```

#### _TCHOccurrenceRecord
**Purpose:** Records actual habit occurrences

**Key Properties:**
- **OccurrenceSeq** (Primary Key) - Unique identifier for occurrence
- **HabitSeq** (Foreign Key) - Reference to habit type
- **LocationSeq** (Foreign Key) - Reference to location
- **OccurrenceDate** - Date when occurrence happened
- **OccurrenceTime** - Time when occurrence happened
- **Description** - Description of the occurrence
- **Intensity** - Intensity level of the occurrence
- **UserSeq** (Foreign Key) - Reference to user who recorded occurrence

**Usage:**
```csharp
// Record new habit occurrence
var occurrence = new _TCHOccurrenceRecord
{
    HabitSeq = habitId,
    LocationSeq = locationId,
    OccurrenceDate = DateTime.Today,
    OccurrenceTime = DateTime.Now.TimeOfDay,
    Description = "Felt stressed during work meeting",
    Intensity = 7,
    UserSeq = currentUser.UserSeq
};

// Get occurrences for specific habit
var occurrences = await context._TCHOccurrenceRecords
    .Where(o => o.HabitSeq == habitId)
    .OrderByDescending(o => o.OccurrenceDate)
    .ToListAsync();
```

#### _TCHOccurrenceAction
**Purpose:** Records actions taken during specific occurrences

**Key Properties:**
- **OccurrenceSeq** (Foreign Key) - Reference to occurrence record
- **ActionSeq** (Foreign Key) - Reference to action type
- **ActionDate** - Date when action was taken
- **Notes** - Additional notes about the action

**Usage:**
```csharp
// Record action taken during occurrence
var occurrenceAction = new _TCHOccurrenceAction
{
    OccurrenceSeq = occurrenceId,
    ActionSeq = actionId,
    ActionDate = DateTime.Now,
    Notes = "Took deep breaths and walked away"
};

// Get actions for specific occurrence
var actions = await context._TCHOccurrenceActions
    .Where(oa => oa.OccurrenceSeq == occurrenceId)
    .Include(oa => oa.ActionSeqNavigation)
    .ToListAsync();
```

#### _TCHOccurrenceCountermeasure
**Purpose:** Records countermeasures applied during occurrences

**Key Properties:**
- **OccurrenceSeq** (Foreign Key) - Reference to occurrence record
- **CountermeasureSeq** (Foreign Key) - Reference to countermeasure type
- **AppliedDate** - Date when countermeasure was applied
- **Effectiveness** - Effectiveness rating of the countermeasure
- **Notes** - Additional notes about the countermeasure

**Usage:**
```csharp
// Record countermeasure application
var occurrenceCountermeasure = new _TCHOccurrenceCountermeasure
{
    OccurrenceSeq = occurrenceId,
    CountermeasureSeq = countermeasureId,
    AppliedDate = DateTime.Now,
    Effectiveness = 8,
    Notes = "Very effective in reducing stress"
};

// Get countermeasures for specific occurrence
var countermeasures = await context._TCHOccurrenceCountermeasures
    .Where(oc => oc.OccurrenceSeq == occurrenceId)
    .Include(oc => oc.CountermeasureSeqNavigation)
    .ToListAsync();
```

#### _TCHOccurrenceState
**Purpose:** Records state changes during occurrences

**Key Properties:**
- **OccurrenceSeq** (Foreign Key) - Reference to occurrence record
- **StateSeq** (Foreign Key) - Reference to state type
- **StateDate** - Date when state was recorded
- **Notes** - Additional notes about the state

**Usage:**
```csharp
// Record state change
var occurrenceState = new _TCHOccurrenceState
{
    OccurrenceSeq = occurrenceId,
    StateSeq = stateId,
    StateDate = DateTime.Now,
    Notes = "Transitioned from stressed to calm"
};

// Get state history for occurrence
var states = await context._TCHOccurrenceStates
    .Where(os => os.OccurrenceSeq == occurrenceId)
    .Include(os => os.StateSeqNavigation)
    .OrderBy(os => os.StateDate)
    .ToListAsync();
```

#### _TCHCorrelationFactor
**Purpose:** Records factors correlated with habit occurrences

**Key Properties:**
- **FactorSeq** (Primary Key) - Unique identifier for correlation factor
- **OccurrenceSeq** (Foreign Key) - Reference to occurrence record
- **FactorType** - Type of correlation factor
- **FactorValue** - Value of the correlation factor
- **CorrelationStrength** - Strength of the correlation
- **Notes** - Additional notes about the factor

**Usage:**
```csharp
// Record correlation factor
var correlationFactor = new _TCHCorrelationFactor
{
    OccurrenceSeq = occurrenceId,
    FactorType = "Weather",
    FactorValue = "Rainy",
    CorrelationStrength = 0.7,
    Notes = "High correlation with rainy weather"
};

// Get correlation factors for occurrence
var factors = await context._TCHCorrelationFactors
    .Where(cf => cf.OccurrenceSeq == occurrenceId)
    .OrderByDescending(cf => cf.CorrelationStrength)
    .ToListAsync();
```

### TwoMites Database Models

#### _TFBible
**Purpose:** Stores Bible study content and references

**Key Properties:**
- **BibleSeq** (Primary Key) - Unique identifier for Bible entry
- **Testament** - Testament (Old/New)
- **Chapter** - Chapter number
- **Verse** - Verse number
- **Descript** - Description or content of the verse
- **CreatedDate** - Date when entry was created
- **ModifiedDate** - Date when entry was last modified

**Usage:**
```csharp
// Create new Bible entry
var bibleEntry = new _TFBible
{
    Testament = "New",
    Chapter = "John",
    Verse = "3:16",
    Descript = "For God so loved the world...",
    CreatedDate = DateTime.Now
};

// Search Bible entries
var entries = await context._TFBibles
    .Where(b => b.Descript.Contains(searchTerm))
    .OrderBy(b => b.Testament)
    .ThenBy(b => b.Chapter)
    .ThenBy(b => b.Verse)
    .ToListAsync();
```

## ViewModels

### HabitOccurrenceViewModel
**Namespace:** `BecomeCaleb_WEB.Models.ViewModel.Caleb.Habit`  
**Purpose:** Data structure for habit occurrence creation

**Properties:**
- **HabitSeq** - Selected habit identifier
- **LocationSeq** - Selected location identifier
- **StateSeq** - Selected state identifier
- **OccurrenceDate** - Date of occurrence
- **OccurrenceTime** - Time of occurrence
- **Description** - Description of the occurrence
- **Intensity** - Intensity level
- **SelectedActions** - List of selected action identifiers
- **SelectedCountermeasures** - List of selected countermeasure identifiers
- **SelectedStates** - List of selected state identifiers

**Usage:**
```csharp
// Create view model for form
var viewModel = new HabitOccurrenceViewModel
{
    OccurrenceDate = DateTime.Today,
    OccurrenceTime = DateTime.Now.TimeOfDay,
    Intensity = 5
};

// Pass to view
return View(viewModel);
```

## Data Relationships

### Habit Tracking Relationships
The habit tracking system uses a complex relationship structure:

1. **Habit → Occurrence**: One habit can have many occurrences
2. **Location → Occurrence**: One location can be associated with many occurrences
3. **State → Occurrence**: One state can be associated with many occurrences
4. **Action → Occurrence**: One action can be taken during many occurrences
5. **Countermeasure → Occurrence**: One countermeasure can be applied to many occurrences

### Financial Relationships
The financial system uses a hierarchical categorization:

1. **Major → Minor**: One major category contains many minor categories
2. **Minor → Ledger**: One minor category contains many ledger entries
3. **User → Ledger**: One user can have many ledger entries

### User Relationships
Users are central to the system:

1. **User → Diary**: One user can have many diary entries
2. **User → Financial**: One user can have many financial transactions
3. **User → Habits**: One user can have many habit occurrences
4. **User → Search**: One user can have many search records

## Data Validation

### Entity Framework Validation
- **Required Fields**: Primary keys and essential properties are required
- **Data Types**: Properties use appropriate .NET data types
- **Constraints**: Database constraints ensure data integrity

### Business Logic Validation
- **Soft Delete**: Entities use IsDeleted flag instead of physical deletion
- **Active Status**: Entities use IsActive flag for status management
- **Audit Trail**: Creation and modification dates are tracked

## Performance Considerations

### Database Optimization
- **Indexing**: Primary keys and foreign keys are indexed
- **Lazy Loading**: Navigation properties are loaded on demand
- **Connection Pooling**: Entity Framework manages connection pooling

### Query Optimization
- **Selective Loading**: Only required properties are loaded
- **Eager Loading**: Related entities are loaded when needed
- **Async Operations**: Database operations use async/await pattern

## Security Features

### Data Access Control
- **User Isolation**: Users can only access their own data
- **Soft Delete**: Data is not physically deleted
- **Audit Logging**: All changes are tracked

### Input Validation
- **Model Validation**: ASP.NET Core ModelState validation
- **Type Safety**: Strong typing prevents injection attacks
- **Parameter Binding**: Explicit parameter binding prevents overposting

---

*This document provides comprehensive information about all models and data structures. For implementation details, refer to the source code and entity configurations.*
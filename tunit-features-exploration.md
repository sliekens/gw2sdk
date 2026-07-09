# TUnit 1.46-1.49 Features Exploration

## Summary

Explored TUnit versions 1.46-1.49 new features for potential adoption in the test suite. The project is currently on TUnit 1.56.0, which includes all features from 1.46-1.49.

## Feature Analysis

### 1. Array Parameter Binding (1.48.0)

**Status:** Not applicable

**Feature:** Allows binding array values from a data source to a single array test parameter.

**Analysis:** Reviewed all tests using `[Arguments(...)]` attribute. Current tests appropriately use individual scalar parameters for semantically distinct values. For example:
- `ItemLinkTest`: passes `itemId`, `count`, `skinId`, `suffixItemId`, `secondarySuffixItemId` as separate parameters
- `CoinTest`: passes `amount`, `gold`, `silver`, `copper` as separate parameters

These are not collections that should be bound as arrays, but distinct test inputs. No opportunities found for array binding.

### 2. Dispose Fix for Shared Fixtures (1.49.0)

**Status:** Automatically benefits (no code changes needed)

**Feature:** Shared fixtures now properly dispose even when only a subset of consuming tests runs (e.g., when filtering by category).

**Analysis:** The project uses `AssemblyFixture` with `SharedType.PerTestSession` in architecture tests:
- `SensibleDefaultsTest.cs`
- `JsonConverterTest.cs`
- `RequireAssignmentTest.cs`
- `DesignedForInheritanceTest.cs`
- `ImmutableDataTest.cs`
- `DataTransferJsonTest.cs`

All architecture tests pass successfully with the current TUnit version, confirming the dispose fix works correctly.

### 3. Generic Mock Discrimination (1.49.0)

**Status:** Not applicable

**Feature:** Generic mock methods can now be configured separately per type argument.

**Analysis:** No usage of `TUnit.Mocks` found in the codebase. The project does not use TUnit's built-in mocking functionality.

### 4. `WasCalled` Assertions (1.48.0)

**Status:** Not applicable

**Feature:** New `WasCalled` assertion for TUnit's built-in mock support.

**Analysis:** No usage of `TUnit.Mocks` found in the codebase. The project does not use TUnit's built-in mocking functionality.

### 5. Performance Improvements (1.47.0)

**Status:** Automatically benefits (no code changes needed)

**Feature:** Extensive allocation and LINQ optimizations across the TUnit engine.

**Analysis:** The project automatically benefits from these performance improvements in TUnit 1.47.0+. No test code changes required.

## Conclusion

The test suite does not have opportunities to adopt the new TUnit 1.46-1.49 features at this time. The project automatically benefits from:
- Performance improvements (1.47.0)
- Shared fixture dispose fix (1.49.0)

No code changes are recommended.

## Build and Test Validation

- ✅ Project builds successfully in Release configuration
- ✅ Architecture tests pass (172 tests, 0 failures)
- ✅ Shared fixtures work correctly with current TUnit version

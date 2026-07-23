# Changelog

## [2.0.0] - 2026-07-23

### Changed
- The task now targets .NET 8.
- Added an **Options** parameter to the main Request task, allowing you to control error handling behaviour: choose whether errors should throw an exception or be returned as a result with `Success = false`, and optionally set a custom error message.

## [1.1.0] - 2024-06-28

### Added
- Added methods: CreateWorkOrder, GenerateServiceRequest, GetWorkOrder, UpdateWorkOrder, DeleteWorkOrder, GetServiceRequest, UpdateServiceRequest, DeleteServiceRequest.
- 
## [1.0.0] - 2024-05-15

### Added
- Initial implementation

# Recruitment Process Portal – Backend

## Overview

This repository contains the **backend service** for the Recruitment Process Portal. The backend is responsible for handling all business logic, data persistence, workflow enforcement, and integrations required to manage the end‑to‑end recruitment lifecycle — from candidate sourcing to hiring and onboarding.

The system is designed as a **clean, modular, and scalable backend** using ASP.NET Core and Entity Framework Core, with a strong focus on correctness, data integrity, and future extensibility.

---

## Technology Stack

* **Framework:** ASP.NET Core (.NET)
* **ORM:** Entity Framework Core
* **Database:** SQL Express for local development
* **Language:** C#
* **Tooling:**

  * `dotnet` CLI
  * Entity Framework migrations
  * SQL Server Management Studio (SSMS)

---
## Core Responsibilities of the Backend

The backend acts as the **single source of truth** for recruitment operations and provides APIs to:

* Manage users, roles, and permissions
* Handle candidate intake and profile management
* Control job creation, publishing, and lifecycle
* Track candidate progression through hiring stages
* Schedule interviews and store feedback
* Generate and manage offers
* Maintain auditability and historical state changes

---

## Key Features

### 1. Authentication & Authorization

* Role‑based access control using predefined user roles
* Secure user account management
* Clear separation between administrative and operational users

### 2. Candidate Management

* Candidate profile creation and updates
* Source tracking (referrals, job portals, events, etc.)
* Resume metadata and document verification
* Skill mapping with experience and verification support

### 3. Job & Position Management

* Job position creation with status tracking
* Skill requirements definition per job
* Linking candidates to jobs through a controlled workflow
* Closure handling with reasons and final outcomes

### 4. Recruitment Workflow Engine

* Centralized **Job–Candidate linking model** to track application state
* Explicit application statuses with controlled transitions
* Status change logging for traceability and audits

### 5. Interview Management

* Interview scheduling across multiple rounds
* Interview type classification (HR, Technical, Managerial, etc.)
* Interview completion tracking

### 6. Screening & Feedback

* Structured screening feedback tied to a job‑candidate link
* Reviewer attribution and timestamps
* Decisions captured without unsafe cascade side effects

### 7. Offer Management

* Offer letter generation and tracking
* Compensation (CTC), joining dates, and offer status

### 8. Event‑Driven Recruitment

* Support for recruitment drives and events
* Job positions linked to recruitment events

---

## Backend Architecture

### Layered Design

* **Controllers (API Layer)**

  * Expose RESTful endpoints
  * Perform request validation

* **Domain Models (Core Layer)**

  * Represent business entities (Candidate, Job, Interview, etc.)
  * Designed around real recruitment workflows

* **Data Access Layer**

  * Entity Framework Core with Fluent API configurations
  * Explicit relationship and delete‑behavior control

* **Configuration Layer**

  * Centralized entity configurations via `IEntityTypeConfiguration<>`
  * Applied automatically through assembly scanning

---

## Database Strategy (High Level)

Although the backend is not database‑centric, it enforces:

* Strong referential integrity
* Explicit delete behaviors (no accidental cascades)
* Audit‑safe relationships
* Migration‑based schema evolution

All schema changes are version‑controlled using EF Core migrations.

---



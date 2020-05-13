export class Entry {
    id: number;
    occuranceDate: Date;
    description: string;
    parentEntryId: number;
    dailyActivityReportId: number;
    entryTypeId: number;
    priorityId: number;
    location: number;
    childEntries: Array<Entry>;
}

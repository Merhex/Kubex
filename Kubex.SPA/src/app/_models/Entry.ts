import { Priority } from './priority';
import { Location } from './location';
import { DailyActivityReport } from './dailyActivityReport';

export class Entry {
    id?: number;
    occuranceDate: Date;
    description: string;
    parentEntry?: Entry;
    dailyActivityReport?: DailyActivityReport;
    priority: string;
    entryType: string;
    location: Location
    childEntries?: Array<Entry>;
}

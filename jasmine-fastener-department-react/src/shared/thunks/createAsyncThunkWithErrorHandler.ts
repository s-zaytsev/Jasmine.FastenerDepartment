import { createAsyncThunk } from '@reduxjs/toolkit';

export function createAsyncThunkWithErrorHandler<Returned, ThunkArg = void>(
  typePrefix: string,
  payloadCreator: (arg: ThunkArg) => Promise<Returned>
) {
  return createAsyncThunk<Returned, ThunkArg>(
    typePrefix,
    async (arg: ThunkArg, { rejectWithValue }) => {
      try {
        return await payloadCreator(arg);
      } catch (error: any) {
        return rejectWithValue(error?.response?.data ?? error);
      }
    }
  );
}

